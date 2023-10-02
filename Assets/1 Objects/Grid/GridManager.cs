using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid.GridInformation;
using Random = UnityEngine.Random;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance = null;

        private GridInformation[,] grid = null;
        public GridInformation[,] GetGrid => grid;

        private SpriteRenderer[,] spriteGrid = null;
        [SerializeField] private SpriteRenderer gridSpritePrefab = null;
        [SerializeField] private Sprite[] gridImages = null;

        [SerializeField] private int width = 0; 
        [SerializeField] private int height = 0;

        [SerializeField] private float size = 1;
        private Vector3 offset;
        private List<GridInformation> availableCells = new List<GridInformation>();

        public Action OnGridUpdated = null;

        public float CellSize { get => size; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            InitializeGrid();
        }

        private void Start()
        {
            OnGridUpdated?.Invoke();
        }

        public GridInformation GetAtPos(int x, int y)
        {
            return grid[y, x];
        }

        public GridInformation GetAtPosTruncate(Vector2 pos)
        {
            return GetAtPos((int)pos.x, (int)pos.y);
        }

        public GridInformation[] FindAllType(GridType type)
        {
            List<GridInformation> allInfo = new List<GridInformation>(); 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (GetAtPos(x, y).type == type)
                    {
                        allInfo.Add(GetAtPos(x, y));
                    }
                }
            }
            return allInfo.ToArray();
        }

        public GridInformation[] FindAllBlockedStatus(bool isBlocked)
        {
            List<GridInformation> allInfo = new List<GridInformation>(); 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (GetAtPos(x, y).isBlocked == isBlocked)
                    {
                        allInfo.Add(GetAtPos(x, y));
                    }
                }
            }
            return allInfo.ToArray();
        }

        public bool IsInsideGrid(Vector2 worldPosition)
        {
            Vector2 bottomLeft = GetAtPos(0, 0).worldPosition - Vector2.one * CellSize / 2f;
            Vector2 topRight = GetAtPos(width - 1, height - 1).worldPosition + Vector2.one * CellSize / 2f;
            return worldPosition.x >= bottomLeft.x
                && worldPosition.y >= bottomLeft.y
                && worldPosition.x <= topRight.x
                && worldPosition.y <= topRight.y;
        }

        public GridInformation GetAtWorldLocation(Vector3 position)
        {
            Vector2 localPos = position - this.transform.position - offset + size / 2f * Vector3.one;
            return GetAtPosTruncate(localPos);
        }

        public Vector2 GetRandomAvailablePosition()
        {
            var cell = availableCells[Random.Range(0, availableCells.Count)];
            return cell.worldPosition;
        }

        public Vector2 WorldToLocal(Vector3 worldPosition)
        {
            return worldPosition - this.transform.position - offset;
        }
        
        public Vector2 WorldToLocal(Vector2Int worldPosition)
        {
            return new Vector3(worldPosition.x, worldPosition.y) - this.transform.position - offset;
        }

        public void SetBlockedState(Vector3 worldPos, bool isBlockedState, GridInformation info = null)
        {
            if (info == null)
                info = GetAtWorldLocation(worldPos);

            info.isBlocked = isBlockedState;

            UpdateAvailableGridInfo(info);
        }

        public void UpdateGridAtWorldPosition(Vector3 worldPos, GridType type)
        {
            var cellInfo = GetAtWorldLocation(worldPos);
            cellInfo.type = type;
            SetBlockedState(worldPos, type == GridType.Item, cellInfo);
        }

        private void InitializeGrid()
        {
            grid = new GridInformation[height, width];
            spriteGrid = new SpriteRenderer[height, width];
            offset.x = transform.position.x - size * width / 2;
            offset.y = transform.position.y - size * height / 2;
            GridInformation info;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    info = new GridInformation(GridType.Empty, (new Vector3(x, y) + offset), new Vector2Int(x, y), false);
                    grid[y, x] = info;
                    availableCells.Add(info);
                    spriteGrid[y, x] = Instantiate(gridSpritePrefab, new Vector3(x, y) + offset, Quaternion.identity, this.transform);
                    spriteGrid[y, x].sprite = gridImages[(x + y) % 2];
                }
            }
        }

        private void UpdateAvailableGridInfo(GridInformation info)
        {
            bool inAvailableList = availableCells.Find(i => info.index == i.index) != null;

            if (info.isBlocked && inAvailableList)
                availableCells.Remove(info);
            else if (!info.isBlocked && !inAvailableList)
                availableCells.Add(info);

            OnGridUpdated?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Gizmos.color = GetAtPos(x,y).isBlocked? Color.red : Color.green;
                        Gizmos.DrawWireCube(GetAtPos(x, y).worldPosition, Vector3.one * (size - 0.1f));
                    }
                }
            }
            else
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Vector2 offset = new Vector2(x + transform.position.x - size * width / 2, y+transform.position.y - size * height / 2);
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(offset, Vector3.one * (size - 0.1f));
                    }
                }
            }
        }
    }

    [Serializable]
    public class GridInformation
    {
        [Serializable]
        public enum GridType { Empty, Item, Character}
        
        public GridType type;
        public Vector2 worldPosition;
        public Vector2Int index;
        public bool isBlocked;

        public GridInformation(GridType type, Vector2 pos, Vector2Int index, bool isBlocked)
        {
            this.type = type;
            this.worldPosition = pos;
            this.index = index;
            this.isBlocked = isBlocked;
        }
    }
}
