using Grid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAStarTargetPoints : MonoBehaviour
{
    public GridInformation[] GetSurroundingAccessPoints()
    {
        List<GridInformation> surroundingPoints = new List<GridInformation>(); 
        Vector2[] surroundingDirections = new Vector2[4] { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1)};
        for (int i = 0; i < surroundingDirections.Length; i++)
        {
            Vector2 pos = (Vector2)this.transform.position + surroundingDirections[i];
            if(pos.x >= 0 && pos.y >= 0 && pos.x < GridManager.Instance.GetGrid.GetLength(1) && pos.y < GridManager.Instance.GetGrid.GetLength(0))
            {
                surroundingPoints.Add(GridManager.Instance.GetAtPosRound((Vector2)this.transform.position + surroundingDirections[i]));
            }
        }
        return surroundingPoints.ToArray();
    }
}
