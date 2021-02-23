using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : Singleton<Grid>
{

    public Vector2Int m_GridDimensions;

    public List<FloorNode> m_GridPathToGoal;

    public GameObject m_PrefabNode;
    public FloorNode[,] m_GridPathArray;
    public List<FloorNode> m_GridPathList;
    



    public FloorNode GetNode(Vector2Int grid)
    {
        if (m_GridPathArray != null)
        {
            return m_GridPathArray[grid.x, grid.y];
        }
        else
        {
            return null;
        }
        
    }
    
    public FloorNode GetNode(int gridX, int gridY)
    {
        if (m_GridPathArray != null)
        {
            return m_GridPathArray[gridX, gridY];
        }
        else
        {
            return null;
        }
        
    }


    
    

    protected static readonly Vector2Int[] _directions =
    {
        new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1)
    };

    public FloorNode CheckNeighborsForLowestNumber(Vector2Int grid)
    {
        float TempHeuristic = 100;
        FloorNode TempNode = null;


        foreach (Vector2Int direction in _directions)
        {
            if ((grid.x + direction.x) < m_GridDimensions.x &&  (grid.y + direction.y)  < m_GridDimensions.y 
                && grid.x + direction.x > -1 && (grid.y + direction.y) > -1)
            {
                FloorNode neighbour = m_GridPathArray[grid.x + direction.x, grid.y + direction.y];
                if (neighbour == null)
                {
                    Debug.Log("neighbour literally doesnt exist");
                    continue;
                }

                if (neighbour.m_Heuristic == -1)
                {
                    Debug.Log("Neighbour heuristic is  -1");
                    continue;
                }

                if (neighbour.m_Heuristic < TempHeuristic)
                {
                    TempHeuristic = neighbour.m_Heuristic;
                    TempNode = neighbour;
                }
            }
        }

        if(TempNode == null)
        {
            Debug.Log("Check Neightbor is null");
            Debug.Break();
        }
        return TempNode;
    }

  

    public bool CheckingGridDimensionBoundrys(Vector2Int aPositionInGrid)
    {
        if (aPositionInGrid.x < m_GridDimensions.x && aPositionInGrid.x >= 0 &&
            aPositionInGrid.y < m_GridDimensions.y && aPositionInGrid.y >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    public List<FloorNode> GetTheLowestH(Vector2Int grid, int aMovement)
    {


        m_GridPathToGoal.Add(CheckNeighborsForLowestNumber(grid));

        for (int i = aMovement; i > 0; i-- )
         {
             if (m_GridPathToGoal[m_GridPathToGoal.Count - 1] == null)
             {
                 Debug.Break();
                 Debug.Log("IT WAS IMPOSSIBLE FOR THIS CHARACTER TO REACH THE POSITION " + grid);
                 break;
             }

             m_GridPathToGoal.Add(CheckNeighborsForLowestNumber(m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_PositionInGrid));
        }
        
        return m_GridPathToGoal;

    }
}
