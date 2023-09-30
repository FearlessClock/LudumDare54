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

        [SerializeField] private int width = 0; 
        [SerializeField] private int height = 0;

        [SerializeField] private int size = 1;

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
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[y, x] = new GridInformation(GridType.Empty, new Vector2(x,y) * size, Random.value > 0.5f); 
                }
            }
        }

        private GridInformation GetAtPos(int x, int y)
        {
            return grid[y, x];
        }

        private GridInformation GetAtPosTruncate(Vector2 pos)
        {
            return GetAtPos((int)pos.x, (int)pos.y);
        }

        private GridInformation[] FindAllType(GridType type)
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

        private GridInformation[] FindAllBlockedStatus(bool isBlocked)
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
