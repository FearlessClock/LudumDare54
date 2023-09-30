using Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBrain : MonoBehaviour
{
    [SerializeField] private CustomerMovementHandler movementHandler = null;
    [SerializeField] private CustomerBuyItemHandler customerBuyItemHandler = null;

    Queue<CustomerAction> actions = new Queue<CustomerAction>();

    public Action<CustomerBrain> OnCustomerDone = null; 

    public void Init(ItemAStarTargetPoints[] itemsToBuy, Vector2 exitPosition)
    {
        for (int i = 0; i < itemsToBuy.Length; i++)
        {
            actions.Enqueue(new WalkAction(GetTargetPoints(itemsToBuy[i].GetSurroundingAccessPoints()), itemsToBuy[i].transform.position, movementHandler));
            actions.Enqueue(new BuyAction(itemsToBuy[i], customerBuyItemHandler));
        }
        actions.Enqueue(new WalkAction(new Vector2Int[1] { new Vector2Int((int)exitPosition.x, (int)exitPosition.y) }, exitPosition, movementHandler));

        StartCoroutine(ActionHandleRoutine());
    }

    private IEnumerator ActionHandleRoutine()
    {
        while(actions.Count > 0)
        {
            CustomerAction action = actions.Dequeue();
            yield return action.DoAction();
        }
        OnCustomerDone?.Invoke(this);
    }

    private Vector2Int[] GetTargetPoints(GridInformation[] gridInformations)
    {
        Vector2Int[] targets = new Vector2Int[gridInformations.Length];
        for (int i = 0; i < gridInformations.Length; i++)
        {
            targets[i] = new Vector2Int((int)gridInformations[i].worldPosition.x, (int)gridInformations[i].worldPosition.y);
        }
        return targets;
    }
}
public enum ActionType{ Walk, Buy}
public interface CustomerAction
{
    ActionType Type { get; }

    IEnumerator DoAction();
}

public class WalkAction : CustomerAction
{
    private Vector2Int[] targetPoints = null;
    private Vector2 center;
    private CustomerMovementHandler movement = null;

    private bool hasFinishedMoving = false;

    public WalkAction(Vector2Int[] itemAStarTargetPoints, Vector2 center, CustomerMovementHandler movement)
    {
        this.targetPoints = itemAStarTargetPoints;
        this.movement = movement;
        this.center = center;
    }

    public ActionType Type => ActionType.Walk;

    public IEnumerator DoAction()
    {
        movement.MoveToPoint(targetPoints, center);
        movement.OnArriveAtSpot += OnDoneMoving;
        while (!hasFinishedMoving)
        {
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void OnDoneMoving()
    {
        hasFinishedMoving = true;
    }
}

public class BuyAction : CustomerAction
{
    private ItemAStarTargetPoints itemToBuy = null;
    private CustomerBuyItemHandler customerBuyItemHandler = null;

    public BuyAction(ItemAStarTargetPoints itemAStarTargetPoints, CustomerBuyItemHandler customerBuyItemHandler)
    {
        this.itemToBuy = itemAStarTargetPoints;
        this.customerBuyItemHandler = customerBuyItemHandler;
    }
    public ActionType Type => ActionType.Buy;

    public IEnumerator DoAction()
    {
        customerBuyItemHandler.ClaimItem(itemToBuy);
        yield return null;
    }
}
