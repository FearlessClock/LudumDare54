using Codice.Client.BaseCommands.Merge;
using DG.Tweening;
using Grid;
using HelperScripts.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public void MoveToPoint(Vector2Int[] targets, Vector2 centerTarget)
    {
        bool res = false;
        path = aStarerer.GetPathTo(GridManager.Instance.GetAtWorldLocation(this.transform.position).index, targets, centerTarget, out res);
        if (!res)
        {
            OnMovementFailed?.Invoke();
            Debug.Log("A customer did not find a path", this);

            return;
        }

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
            OnArriveAtSpot.Invoke();
        }
    }
}
