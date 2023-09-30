using Grid;
using HelperScripts.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomerBrain : MonoBehaviour
{
    [SerializeField] private CustomerMovementHandler movementHandler = null;
    [SerializeField] private CustomerBuyItemHandler customerBuyItemHandler = null;
    [SerializeField] private CustomerComplainPopupHandler customerComplainHandler = null;
    [SerializeField] private EventScriptable onFileComplaint = null;

    Queue<CustomerAction> actions = new Queue<CustomerAction>();

    public Action<CustomerBrain> OnCustomerDone = null;

    private Coroutine actionRoutine = null;
    private bool actionLoopStatus = true;

    private bool hasRetried = false;

    private Vector2 exitPosition;
    private Vector2 entrancePosition;

    private GridInformation[] accessPoints = new GridInformation[0];

    public void Init(ItemAStarTargetPoints[] itemsToBuy, Vector2 exitPosition)
    {
        accessPoints = itemsToBuy[1].GetSurroundingAccessPoints();
        this.exitPosition = exitPosition;
        this.entrancePosition = this.transform.position;
        for (int i = 0; i < itemsToBuy.Length; i++)
        {
            actions.Enqueue(new WalkAction(GetTargetPoints(itemsToBuy[i].GetSurroundingAccessPoints()), itemsToBuy[i].transform.position, movementHandler));
            actions.Enqueue(new BuyAction(itemsToBuy[i], customerBuyItemHandler));
        }
        actions.Enqueue(new WalkAction(new Vector2Int[1] { new Vector2Int((int)exitPosition.x, (int)exitPosition.y) }, exitPosition, movementHandler));

        customerBuyItemHandler.Init(itemsToBuy);

        actionRoutine = StartCoroutine(ActionHandleRoutine());
    }

    private void ActionFailed()
    {
        actionLoopStatus = false;
        actions.Clear();
        if (!hasRetried)
        {
            actions.Enqueue(new ComplainAction(customerComplainHandler));
            ItemAStarTargetPoints[] items = customerBuyItemHandler.GetRemainingItems;
            for (int i = 0; i < items.Length; i++)
            {
                actions.Enqueue(new WalkAction(GetTargetPoints(items[i].GetSurroundingAccessPoints()), items[i].transform.position, movementHandler));
                actions.Enqueue(new BuyAction(items[i], customerBuyItemHandler));
            }
            actions.Enqueue(new WalkAction(new Vector2Int[1] { new Vector2Int((int)exitPosition.x, (int)exitPosition.y) }, exitPosition, movementHandler));
            hasRetried = true;
        }
        else
        {
            actions.Enqueue(new WalkAction(new Vector2Int[1] { new Vector2Int((int)entrancePosition.x, (int)entrancePosition.y) }, entrancePosition, movementHandler));
            actions.Enqueue(new FileComplaintAction(onFileComplaint));
            hasRetried = true;
        }
        if (actionRoutine != null)
        {
            StopCoroutine(actionRoutine);
        }
        actionLoopStatus = true;
        actionRoutine = StartCoroutine(ActionHandleRoutine());
    }

    private IEnumerator ActionHandleRoutine()
    {
        while(actions.Count > 0)
        {
            CustomerAction action = actions.Dequeue();
            action.OnActionFailed += ActionFailed;
            yield return action.DoAction();
            action.OnActionFailed -= ActionFailed;
            if (!actionLoopStatus)
            {
                break;
            }
        }
        if (!actionLoopStatus)
        {
            OnCustomerDone?.Invoke(this);
        }
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

    private void OnDrawGizmos()
    {
        for (int i = 0; i < accessPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(accessPoints[i].worldPosition, 0.5f);
        }
    }
}
public enum ActionType{ Walk, Buy}
public interface CustomerAction
{
    ActionType Type { get; }
    Action OnActionFailed { get; set; }

    IEnumerator DoAction();
}

public class WalkAction : CustomerAction
{
    private Vector2Int[] targetPoints = null;
    private Vector2 center;
    private CustomerMovementHandler movement = null;

    private bool hasFinishedMoving = false;

    private Action failedAction = null;

    public WalkAction(Vector2Int[] itemAStarTargetPoints, Vector2 center, CustomerMovementHandler movement)
    {
        this.targetPoints = itemAStarTargetPoints;
        this.movement = movement;
        this.center = center;
    }

    public ActionType Type => ActionType.Walk;

    public Action OnActionFailed
    {
        get { return failedAction; }
        set { failedAction += value; }
    }

    public IEnumerator DoAction()
    {
        movement.MoveToPoint(targetPoints, center);
        movement.OnArriveAtSpot += OnDoneMoving;
        movement.OnMovementFailed += OnMovementFailed;
        while (!hasFinishedMoving)
        {
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void OnMovementFailed()
    {
        failedAction?.Invoke();
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

    private Action failedAction = null;

    public BuyAction(ItemAStarTargetPoints itemAStarTargetPoints, CustomerBuyItemHandler customerBuyItemHandler)
    {
        this.itemToBuy = itemAStarTargetPoints;
        this.customerBuyItemHandler = customerBuyItemHandler;
    }
    public ActionType Type => ActionType.Buy;

    public Action OnActionFailed
    {
        get { return failedAction; }
        set { failedAction += value; }
    }

    public IEnumerator DoAction()
    {
        customerBuyItemHandler.ClaimItem(itemToBuy);
        yield return null;
    }
}
public class ComplainAction : CustomerAction
{
    private float waitTime = 5;

    private Action failedAction = null;

    private CustomerComplainPopupHandler customerComplainHandler;

    public ComplainAction(CustomerComplainPopupHandler customerComplainHandler)
    {
        this.customerComplainHandler = customerComplainHandler;
    }

    public ActionType Type => ActionType.Buy;

    public Action OnActionFailed
    {
        get { return failedAction; }
        set { failedAction += value; }
    }

    public IEnumerator DoAction()
    {
        Debug.Log("Do complain action");
        customerComplainHandler.Complain();
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Complaining done");

        yield return null;
    }
}
public class FileComplaintAction : CustomerAction
{
    private EventScriptable onFileComplaint = null;

    private Action failedAction = null;

    public FileComplaintAction(EventScriptable onFileComplaint)
    {
        this.onFileComplaint = onFileComplaint;
    }

    public ActionType Type => ActionType.Buy;

    public Action OnActionFailed
    {
        get { return failedAction; }
        set { failedAction += value; }
    }

    public IEnumerator DoAction()
    {
        onFileComplaint.Call();

        yield return null;
    }
}
