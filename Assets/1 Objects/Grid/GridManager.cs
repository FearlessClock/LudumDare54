using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid.GridInformation;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance = null;

        private GridInformation[,] grid = null;
        public GridInformation[,] GetGrid => grid;

        [SerializeField] private int width = 0; 
        [SerializeField] private int height = 0;

        [SerializeField] private int size = 1;
        public int CellSize { get => size; }
        private Vector3 offset;

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

            grid = new GridInformation[height, width];
            offset.x = transform.position.x - size * width /2;
            offset.y = transform.position.y - size * height / 2;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[y, x] = new GridInformation(GridType.Empty,(new Vector3(x,y) + offset) * size, false); 
                }
            }
        }

        public GridInformation GetAtPos(int x, int y)
        {
            return grid[y, x];
        }

        public GridInformation GetAtPosTruncate(Vector2 pos)
        {
            return GetAtPos((int)pos.x, (int)pos.y);
        }

        public GridInformation GetAtPosRound(Vector2 pos)
        {
            return GetAtPos(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
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
            Vector2 bottomLeft = GetAtPos(0, 0).position - Vector2.one * CellSize / 2f;
            Vector2 topRight = GetAtPos(width - 1, height - 1).position + Vector2.one * CellSize / 2f;
            return worldPosition.x >= bottomLeft.x
                && worldPosition.y >= bottomLeft.y
                && worldPosition.x <= topRight.x
                && worldPosition.y <= topRight.y;
        }

        public GridInformation GetAtWorldLocation(Vector3 position)
        {
            Vector2 localPos = position - this.transform.position - offset;
            return GetAtPosRound(localPos);
        }

        public void SetBlockedState(Vector3 worldPos, bool isBlockedState)
        {
            GetAtWorldLocation(worldPos).isBlocked = isBlockedState;
        }

        public void SetGridType(Vector3 worldPos, GridType type)
        {
            GetAtWorldLocation(worldPos).type = type;
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
                        Gizmos.DrawWireCube(GetAtPos(x, y).position, Vector3.one * (size - 0.1f));
                    }
                }
            }
        }
    }
     
    public class GridInformation
    {
        public enum GridType { Empty, Item, Character}
        public GridType type;
        public Vector2 position;
        public bool isBlocked;

        public GridInformation(GridType type, Vector2 pos, bool isBlocked)
        {
            this.type = type;
            this.position = pos;
            this.isBlocked = isBlocked;
        }
    }
}
