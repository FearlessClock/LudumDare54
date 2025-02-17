using DG.Tweening;
using Grid;
using System;
using UnityEngine;

public class CustomerMovementHandler : MonoBehaviour
{
    private Vector2[] path = null;
    
    private Vector2 targetPosition;
    private int pathStep = 0;

    private bool hasPath = false;

    [SerializeField] private float moveDuration = 0.05f;
    
    private bool isMoving = false;
    public Action OnArriveAtSpot = null;
    public Action OnMovementFailed = null;

    AStarStuff.AStar aStarerer = new AStarStuff.AStar();

    private Vector2[] targets = null;
    private Vector2 centerTarget;
    public void MoveToPoint(Vector2[] targets, Vector2 centerTarget)
    {
        this.targets = targets;
        this.centerTarget = centerTarget;

        bool res = false;
        path = aStarerer.GetPathTo(GridManager.Instance.GetAtWorldLocation(this.transform.position).index, targets, centerTarget, out res);
        if (!res)
        {
            OnMovementFailed?.Invoke();
            Debug.Log("A customer did not find a path", this);

            return;
        }

        GridManager.Instance.OnGridUpdated += OnGridUpdate;

        if(path.Length > 0)
        {
            targetPosition = path[1];
            pathStep = 0;
            hasPath = true;
        }
        else
        {
            DoneMoving();
        }
    }

    private void OnDestroy()
    {
        GridManager.Instance.OnGridUpdated -= OnGridUpdate;
    }

    private void OnGridUpdate()
    {
        bool isBlocked = false;
        for (int i = 0; i < path.Length; i++)
        {
            if (GridManager.Instance.GetAtWorldLocation(path[i]).isBlocked)
            {
                isBlocked = true;
                break;
            }
        }

        if (!isBlocked)
            return;

        bool res = false;
        path = aStarerer.GetPathTo(GridManager.Instance.GetAtWorldLocation(this.transform.position).index, targets, centerTarget, out res);
        if (!res)
        {
            OnMovementFailed?.Invoke();
            Debug.Log("A customer did not find a path", this);

            return;
        }

        if (path.Length > 0)
        {
            targetPosition = path[1];
            pathStep = 0;
            hasPath = true;
        }
        else
        {
            DoneMoving();
        }
    }

    private void Update()
    {
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
            OnArriveAtSpot?.Invoke();
        }
    }
}
