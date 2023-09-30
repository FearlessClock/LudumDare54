using Codice.Client.BaseCommands.Merge;
using DG.Tweening;
using Grid;
using HelperScripts.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CustomerMovementHandler : MonoBehaviour
{
    // Targets
    // Path
    //Checking grid
    // Move

    private ItemAStarTargetPoints[] itemsToBuy = null;
    private Vector2[] path = null;
    
    private Vector2 targetPosition;
    private Vector2 exitPosition;
    private int pathStep = 0;
    private int currentStepInTaskList = 0;
    private Queue<ItemAStarTargetPoints> taskList = new Queue<ItemAStarTargetPoints>();

    private bool hasPath = false;

    [SerializeField] private float moveDuration = 0.05f;
    
    private bool hasSeCasser = false;
    private bool isMoving = false;
    public Action OnArriveAtSpot = null;
    public Action OnSeCasser = null;

    AStarStuff.AStar aStarerer = new AStarStuff.AStar();

    private bool isWaiting = false;
    private float waitingTimer = 5;
    [SerializeField] private float waitTime = 5;
    [SerializeField] private int waitCycles = 2;
    [SerializeField] private EventScriptable onFileComplaint = null;

    public void Init(ItemAStarTargetPoints[] itemsToBuy, Vector2 exitPosition)
    {
        this.itemsToBuy = itemsToBuy;

        for (int i = 0; i < itemsToBuy.Length; i++)
        {
            taskList.Enqueue(itemsToBuy[i]);
        }
        this.exitPosition = exitPosition;
        FindPath();
    }

    public void FindPath()
    {
        Vector2Int[] targets = GetTargetPoints(taskList.Peek().GetSurroundingAccessPoints());
        path = aStarerer.GetPathTo(GetIntVector2(GridManager.Instance.GetAtWorldLocation(this.transform.position).position), targets, taskList.Peek().transform.position);

        if (path.Length == 0)
        {
            // TODO: Complain flow
            waitCycles--;
            if (waitCycles > 0)
            {
                isWaiting = true;
                waitingTimer = 5f;
            }else
                onFileComplaint?.Call();

            Debug.Log("A customer did not find a path", this);

            return;
        }

        targetPosition = path[1];
        pathStep = 0;
        hasPath = true;
    }

    private Vector2Int[] GetTargetPoints(GridInformation[] gridInformations)
    {
        Vector2Int[] targets = new Vector2Int[gridInformations.Length];
        for (int i = 0; i < gridInformations.Length; i++)
        {
            targets[i] = new Vector2Int(Mathf.RoundToInt(gridInformations[i].position.x), Mathf.RoundToInt(gridInformations[i].position.y));
        }
        return targets;
    }

    private Vector2Int GetIntVector2(Vector2 vec)
    {
        return new Vector2Int(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y));
    }

    private void Update()
    {
        if(isWaiting)
        {
            waitingTimer -= Time.deltaTime;
            if( waitingTimer <= 0)
            {
                isWaiting = false;
                FindPath();
            }
        }

        if (!hasPath) return;

        if (!isMoving)
        {
            isMoving = true;
            transform.DOMove(targetPosition, moveDuration).SetEase(Ease.Linear).OnComplete(DoneMoving);
        }
    }

    private void DoneMoving()
    {
        isMoving = false;
        pathStep++;
        if (path.Length > pathStep)
        {
            targetPosition = path[pathStep];
        }
        else
        {
            pathStep = 0;
            hasPath = false;
            if(taskList.Count == 0)
            {
                if (!hasSeCasser)
                {
                    hasSeCasser = true;
                    OnSeCasser?.Invoke();
                }
                return;
            }
            GetNewPath();

            OnArriveAtSpot.Invoke();
        }
    }

    private void GetNewPath()
    {
        taskList.Dequeue();
        if(taskList.Count > 0)
        {
            Vector2Int[] targets = GetTargetPoints(taskList.Peek().GetSurroundingAccessPoints());
            path = aStarerer.GetPathTo(GetIntVector2(GridManager.Instance.GetAtWorldLocation(this.transform.position).position), targets, taskList.Peek().transform.position);
        }
        else
        {
            path = aStarerer.GetPathTo(GetIntVector2(GridManager.Instance.GetAtWorldLocation(this.transform.position).position),
                                                new Vector2Int[1] { new Vector2Int((int)exitPosition.x, (int)exitPosition.y) }, exitPosition);
        }
        hasPath = true;
    }

    private void OnDrawGizmosSelected()
    {
        return;
        if (!Application.isPlaying) return;
        if (path.Length == 0) return;

        for (int i = 1; i < path.Length; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(path[i-1], path[i]);
        }
    }
}
