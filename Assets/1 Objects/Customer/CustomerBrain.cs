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

    private CustomerSpawner customerSpawner;
    

    Queue<CustomerAction> actions = new Queue<CustomerAction>();

    public Action<CustomerBrain> OnCustomerDone = null;

    private Coroutine actionRoutine = null;
    private bool actionLoopStatus = true;

    private bool hasRetried = false;

    private Vector2 exitPosition;
    private Vector2 entrancePosition;

    public void Init(Item[] itemsToBuy, Vector2 exitPosition)
    {
        this.exitPosition = exitPosition;
        this.entrancePosition = this.transform.position;
        for (int i = 0; i < itemsToBuy.Length; i++)
        {
            actions.Enqueue(new WalkAction(GetTargetPoints(itemsToBuy[i].GetLayoutPositions()), itemsToBuy[i].transform.position, movementHandler));
            actions.Enqueue(new BuyAction(itemsToBuy[i], customerBuyItemHandler));
        }
        actions.Enqueue(new WalkAction(new Vector2[1] { new Vector2((int)exitPosition.x, (int)exitPosition.y) }, exitPosition, movementHandler));

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
            Item[] items = customerBuyItemHandler.GetRemainingItems;
            for (int i = 0; i < items.Length; i++)
            {
                actions.Enqueue(new WalkAction(GetTargetPoints(items[i].GetAdjacentLayoutPositions()), items[i].transform.position, movementHandler));
                actions.Enqueue(new BuyAction(items[i], customerBuyItemHandler));
            }
            actions.Enqueue(new WalkAction(new Vector2[1] { new Vector2Int((int)exitPosition.x, (int)exitPosition.y) }, exitPosition, movementHandler));
            hasRetried = true;
        }
        else
        {
            actions.Enqueue(new WalkAction(new Vector2[1] { new Vector2Int((int)entrancePosition.x, (int)entrancePosition.y) }, entrancePosition, movementHandler));
            actions.Enqueue(new FileComplaintAction(onFileComplaint, customerBuyItemHandler));
            
            Item[] items = customerBuyItemHandler.GetRemainingItems;
            for (int i = 0; i < items.Length; i++)
            {
                customerSpawner.RecovObject(items[i]);
            }

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

    private Vector2[] GetTargetPoints(Vector2[] gridPositions)
    {
        Vector2[] targets = new Vector2[gridPositions.Length];
        for (int i = 0; i < gridPositions.Length; i++)
        {
            targets[i] = new Vector2Int((int)gridPositions[i].x, (int)gridPositions[i].y);
        }
        return targets;
    }

    public void SetCustomerSpawner(CustomerSpawner spawner)
    {
        customerSpawner = spawner;
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
    private Vector2[] targetPoints = null;
    private Vector2 center;
    private CustomerMovementHandler movement = null;

    private bool hasFinishedMoving = false;

    private Action failedAction = null;

    public WalkAction(Vector2[] itemAStarTargetPoints, Vector2 center, CustomerMovementHandler movement)
    {
        this.targetPoints = itemAStarTargetPoints; //available adjacents cell must be sent through
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
        movement.OnArriveAtSpot += OnDoneMoving;
        movement.OnMovementFailed += OnMovementFailed;
        movement.MoveToPoint(targetPoints, center);
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
    private Item itemToBuy = null;
    private CustomerBuyItemHandler customerBuyItemHandler = null;

    private Action failedAction = null;

    public BuyAction(Item itemAStarTargetPoints, CustomerBuyItemHandler customerBuyItemHandler)
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
    private CustomerBuyItemHandler customerBuyItemHandler = null;
    private Action failedAction = null;

    public FileComplaintAction(EventScriptable onFileComplaint, CustomerBuyItemHandler customerBuyItemHandler)
    {
        this.onFileComplaint = onFileComplaint;
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
        customerBuyItemHandler.ReturnItems();
        onFileComplaint.Call();

        yield return null;
    }
}
