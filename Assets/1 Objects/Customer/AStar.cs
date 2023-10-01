using Codice.Client.Common;
using Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarStuff
{
    public class AStar
    {
        private List<Node> open = null;
        private List<Node> close = null;

        private Node[,] grid = null;

        public AStar() 
        {
            open = new List<Node>();
            close = new List<Node>();
        }

        public Vector2[] GetPathTo(Vector2Int startIndex, Vector2[] endAccessibleTargetsWorld, Vector2 centerPointWorld, out bool res)
        {
            res = false;
            for (int i = 0; i < endAccessibleTargetsWorld.Length; i++)
            {
                if (startIndex == GridManager.Instance.WorldToLocal(endAccessibleTargetsWorld[i]))
                {
                    res = true;
                    return new Vector2[0];
                }
            }

            GenerateGrid();

            open.Clear();
            close.Clear();

            open.Add(grid[startIndex.y, startIndex.x]);

            Node currentNode;
            int loopCounter = 10000;
            while (open.Count > 0)
            {
                loopCounter--;
                if(loopCounter < 0) { return null; }
                open = open.OrderBy(node => node.f).ToList();
                
                currentNode = open[0];
                open.RemoveAt(0);
                close.Add(currentNode);
                if (IsAtOneOfPoints(endAccessibleTargetsWorld, currentNode.position))
                {
                    res = true;
                    return GeneratePathFrom(currentNode, startIndex);
                }


                Node[] neighbors = FindNeighbors(currentNode);

                for (int i = 0; i < neighbors.Length; i++)
                {
                    neighbors[i].g = currentNode.g + 1;
                    neighbors[i].h = ManhattenDistance(neighbors[i].position, GridManager.Instance.WorldToLocal(centerPointWorld));
                    neighbors[i].f = neighbors[i].g + neighbors[i].h;

                    Node openNode = OpenContains(neighbors[i]);
                    if (openNode != null &&  openNode.f > neighbors[i].f)
                    {
                        open.Remove(openNode);
                    }
                    Node closeNode = CloseContains(neighbors[i]);
                    if (closeNode != null && closeNode.f > neighbors[i].f)
                    {
                        close.Remove(closeNode);
                    }
                    if(closeNode == null && openNode == null)
                    {
                        neighbors[i].parentPos = currentNode.position;
                        open.Add(neighbors[i]);
                    }
                }
            }

            res = false;
            return new Vector2[0];
        }

        private bool IsAtOneOfPoints(Vector2[] end, Vector2 currentPos)
        {
            for (int i = 0; i < end.Length; i++)
            {
                if (GridManager.Instance.WorldToLocal(end[i]) == currentPos)
                {
                    return true;
                }
            }
            return false;
        }

        private Vector2[] GeneratePathFrom(Node node, Vector2Int start)
        {
            List<Vector2> path = new List<Vector2>();
            path.Add(GridManager.Instance.GetAtPos((int)node.position.x, (int)node.position.y).worldPosition);
            int counter = 1000;
            while (node.position != start)
            {
                counter--;
                if (counter < 0) throw new Exception("Too big loop");
                path.Add(GridManager.Instance.GetAtPos((int)node.parentPos.x, (int)node.parentPos.y).worldPosition);
                node = grid[(int)node.parentPos.y, (int)node.parentPos.x];
            }
            path.Reverse();
            return path.ToArray();
        }

        private Node CloseContains(Node node)
        {
            for (int i = 0; i < close.Count; i++)
            {
                if (close[i].position == node.position)
                {
                    return close[i];
                }
            }
            return null;
        }

        private Node OpenContains(Node node)
        {
            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].position == node.position)
                {
                    return open[i];
                }
            }
            return null;
        }

        private float ManhattenDistance(Vector2 position, Vector2 end)
        {
            return Mathf.Abs(position.x - end.x) + Mathf.Abs(position.y - end.y);
        }

        private void GenerateGrid()
        {
            grid = new Node[GridManager.Instance.GetGrid.GetLength(0), GridManager.Instance.GetGrid.GetLength(1)];
            for (int y = 0; y < GridManager.Instance.GetGrid.GetLength(0); y++)
            {
                for (int x = 0; x < GridManager.Instance.GetGrid.GetLength(1); x++)
                {
                    grid[y,x] = new Node(new Vector2(x,y), GridManager.Instance.GetGrid[y,x].isBlocked, 0,0,0);
                }
            }
        }

        private Node[] FindNeighbors(Node currentNode)
        {
            List<Node> nodes = new List<Node>();
            Vector2Int[] directions = new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1)};

            int x = 0;
            int y = 0;

            for (int i = 0; i < directions.Length; i++)
            {
                x = directions[i].x + Mathf.RoundToInt(currentNode.position.x);
                y = directions[i].y + Mathf.RoundToInt(currentNode.position.y);

                if(x >= 0 && x < grid.GetLength(1) && y >= 0 && y < grid.GetLength(0) && !grid[y,x].isBlocked)
                {
                    nodes.Add(grid[y, x]);
                }
            }

            return nodes.ToArray();
        }
    }

    public class Node
    {
        public Vector2 position;
        public Vector2 parentPos;
        public bool isBlocked;
        public float f;
        public float g;
        public float h;

        public Node(Vector2 pos, bool isBlocked, int f, int g, int h)
        {
            position = pos;
            this.isBlocked = isBlocked;

            this.f = f;
            this.g = g; 
            this.h = h; 
        }
    }
}

