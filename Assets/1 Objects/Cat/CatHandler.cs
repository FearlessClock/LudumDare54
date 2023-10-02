using AStarStuff;
using DG.Tweening;
using Grid;
using System;
using System.Collections;
using UnityEngine;

public class CatHandler : Block
{
    [Serializable]
    public struct CatLayout
    {
        public ItemLayout layouts;
        public Sprite sprite;
        public Vector2 spriteOffset;
    }

    private enum CatState
    {
        Sleeping,
        Moving
    }

    [SerializeField] private CatLayout[] catLayouts;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int catIndex = 2;

    private CatState catState = CatState.Sleeping;
    [SerializeField] private float changePositionTime = 10;
    private float timer = 0;
    AStar aStar;
    private Vector2[] path;
    private int pathStep = 0;
    private bool isMoving = false;
    private bool IsLookingForPath = false;
    [SerializeField] private float timeToMove = 0.8f;

    private void Start()
    {
        aStar = new AStar();
        for (int i = 0; i < catLayouts.Length; i++)
        {
           if (!catLayouts[i].layouts.Contains(Vector3.zero))
                catLayouts[i].layouts.AddCenterBlock();
        }
        UpdateCatLayout();
        timer = changePositionTime + UnityEngine.Random.Range(-2f, 2f);
    }

    private void UpdateCatLayout()
    {
        float cellSize = GridManager.Instance.CellSize;
        EmptyOldPositions(cellSize);
        spriteRenderer.sprite = catLayouts[catIndex].sprite;
        spriteRenderer.transform.localPosition = catLayouts[catIndex].spriteOffset;
        blockLayoutOffset = catLayouts[catIndex].layouts.Positions;
        Init();
        UpdateNewPositions(cellSize, transform.position);
        lastPosition = transform.position;
        wasPlacedOnce = true;
    }

    private void Update()
    {
        if (catState == CatState.Sleeping)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                ChangeState();
            }
        }
        else if(catState == CatState.Moving)
        {
            if (IsLookingForPath) return;
            if (!isMoving)
            {
                this.transform.DOMove(path[pathStep], timeToMove).SetEase(Ease.Linear).OnComplete(MovementDone);
                isMoving = true;
            }
        }
    }

    private void MovementDone()
    {
        UpdateCatLayout();
        isMoving = false;
        pathStep++;
        if (pathStep >= path.Length)
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        switch (catState)
        {
            case CatState.Sleeping:
                if (UnityEngine.Random.Range(0f, 1f) > .0f)
                {
                    catState = CatState.Moving;
                    StartCoroutine(GetCatPath());
                }
                else
                {
                    int i = UnityEngine.Random.Range(0, catLayouts.Length);
                    if (i == catIndex)
                        i = UnityEngine.Random.Range(0, catLayouts.Length);

                    catIndex = i;
                    UpdateCatLayout();

                }
                timer = changePositionTime;
                break;
            
            case CatState.Moving:
                catState = CatState.Sleeping;
                timer = changePositionTime + UnityEngine.Random.Range(-2f, 2f);
                break;
        }
    }

    private IEnumerator GetCatPath()
    {
        pathStep = 0;
        Debug.Log("try get pos");
        bool res = false;
        Vector2 targetPos = Vector2.zero;
        int counter = 100;
        IsLookingForPath = true;
        do
        {
            targetPos = GridManager.Instance.GetRandomCellPosition();
            if (GridManager.Instance.GetAtWorldLocation(targetPos).isBlocked)
                break;
            counter--;
            if (counter < 0) break;
            path = aStar.GetPathTo(GridManager.Instance.GetAtWorldLocation(transform.position).index, new Vector2[1] { targetPos }, targetPos, out res);
            yield return new WaitForSeconds(0.2f);
        } while (!res);
        IsLookingForPath = false;
        if (!res || path != null && path.Length == 0)
        {
            Debug.Log("Empty path");
            catState = CatState.Sleeping;
        }
    }
}