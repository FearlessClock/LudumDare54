using Codice.Client.BaseCommands.Merge;
using DG.Tweening;
using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovementHandler : MonoBehaviour
{
    // Targets
    // Path
    //Checking grid
    // Move

    private Transform[] itemsToBuy = null;
    private Vector2[] path = null;
    
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private int pathStep = 0;

    private bool hasPath = false;

    [SerializeField] private float moveDuration = 0.05f;
    
    private bool isMoving = false;

    public void Init(Transform[] itemsToBuy, Vector2 exitPosition)
    {
        this.itemsToBuy = itemsToBuy;

        currentPosition = transform.position;

        AStarStuff.AStar aStarerer = new AStarStuff.AStar();
        List<Vector2> bigPath = new List<Vector2>();
        bigPath.AddRange(aStarerer.GetPathTo(GetIntVector2(GridManager.Instance.GetAtWorldLocation(currentPosition).position),
                                            GetIntVector2(GridManager.Instance.GetAtWorldLocation(itemsToBuy[0].position).position)));
        for (int i = 0; i < itemsToBuy.Length-1; i++)
        {
            bigPath.AddRange(aStarerer.GetPathTo(GetIntVector2(GridManager.Instance.GetAtWorldLocation(itemsToBuy[i].position).position),
                                                GetIntVector2(GridManager.Instance.GetAtWorldLocation(itemsToBuy[i+1].position).position)));
        }
        bigPath.AddRange(aStarerer.GetPathTo(GetIntVector2(GridManager.Instance.GetAtWorldLocation(itemsToBuy[^1].position).position),
                                            GetIntVector2(GridManager.Instance.GetAtWorldLocation(exitPosition).position)));
        path = bigPath.ToArray();
        if (path.Length == 0)
        {
            // TODO: Complain flow

            Debug.Log("A customer did not find a path", this);

            return;
        }

        targetPosition = path[1];
        pathStep = 0;
        hasPath = true;
    }

    private Vector2Int GetIntVector2(Vector2 vec)
    {
        return new Vector2Int(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y));
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
            hasPath = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (path.Length == 0) return;
        if (Application.isPlaying) return;

        for (int i = 1; i < path.Length; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(path[i-1], path[i]);
        }
    }
}
