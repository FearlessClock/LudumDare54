using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
                    grid[y, x] = new GridInformation(GridType.Empty,(new Vector3(x,y) + offset), new Vector2Int(x,y), false); 
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

        public GridInformation GetAtWorldLocation(Vector3 position)
        {
            Vector2 localPos = position - this.transform.position - offset;
            return GetAtPosTruncate(localPos);
        }

        public Vector2 WorldToLocal(Vector3 worldPosition)
        {
            return worldPosition - this.transform.position - offset;
        }
        
        public Vector2 WorldToLocal(Vector2Int worldPosition)
        {
            return new Vector3(worldPosition.x, worldPosition.y) - this.transform.position - offset;
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
                        Gizmos.DrawWireCube(GetAtPos(x, y).worldPosition, Vector3.one * (size - 0.1f));
                    }
                }
            }
        }
    }
     
    public class GridInformation
    {
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
