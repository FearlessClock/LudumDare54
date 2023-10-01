using Grid;
using System;
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

    private void Start()
    {

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
                int i = UnityEngine.Random.Range(0, catLayouts.Length);
                if(i == catIndex)
                    i = UnityEngine.Random.Range(0, catLayouts.Length);

                catIndex = i;
                UpdateCatLayout();
                timer = changePositionTime + UnityEngine.Random.Range(-2f, 2f);

            }
        }
    }
}