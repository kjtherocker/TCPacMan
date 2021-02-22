using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PrefabUtility;

[ExecuteInEditMode]
[System.Serializable]
public class FloorManager : MonoBehaviour
{

    //TODO spawn the arena somewhere and latch onto it

    public Floor m_FloorCore;
    public FloorNode[] m_FloorNodes;
    public FloorNode m_FloorNodePrefab;
    private Dictionary<Floor.FloorDirections, Vector2Int>  m_CardinalPositions;

    public GameObject m_Node;

    
    public void Initialize()
    {
        SpawnGimmicks();
        m_CardinalPositions = new Dictionary<Floor.FloorDirections, Vector2Int>();
        m_CardinalPositions.Add(Floor.FloorDirections.Up, new Vector2Int(-1,0));
        m_CardinalPositions.Add(Floor.FloorDirections.Down, new Vector2Int(1,0));
        m_CardinalPositions.Add(Floor.FloorDirections.Left, new Vector2Int(0,-1));
        m_CardinalPositions.Add(Floor.FloorDirections.Right, new Vector2Int(0,1));
    }


    

    
    public void CreateGrid()
    {
        m_FloorCore.Intialize();

        if (m_FloorNodes.Length > 0 && m_FloorNodes != null)
        {
            for (int i = m_FloorNodes.Length -1; i >= 0; i--)
            {
                if (m_FloorNodes[i] == null)
                {
                    continue;
                }

                DestroyImmediate(m_FloorNodes[i].gameObject);
            }
        }


        m_FloorNodes = new FloorNode[m_FloorCore.GridDimensionX * m_FloorCore.GridDimensionY];
        SetLevelNodes(m_FloorCore.FloorBlueprint);
      

    }



    public void SpawnGimmicks()
    {
        m_FloorCore.initializeGimmicks();

    }



    public void SetLevelNodes(short[] aLevelBlueprint)
    {
        for (int x = 0; x < m_FloorCore.GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (aLevelBlueprint[LevelIndex] == (short) Floor.FloorDirections.Empty)
                {
                    continue;
                }

                SpawnNode(x , y,LevelIndex );
                
                m_FloorNodes[LevelIndex].Initialize(aLevelBlueprint[LevelIndex]);
            }
        }
    
    }



    
    public FloorNode GetNode(Vector2Int CurrentPosition,Floor.FloorDirections TargetDirection)
    {
        Vector2Int FinalPosition = new Vector2Int(CurrentPosition.x + m_CardinalPositions[TargetDirection].x,
            CurrentPosition.y + m_CardinalPositions[TargetDirection].y );
        
        int FinalIndex = m_FloorCore.GetIndex( FinalPosition.x,FinalPosition.y) ;
        
        Debug.Log("Current position " + CurrentPosition + " TargetDirection " + m_CardinalPositions[TargetDirection] + " Final index: " + FinalIndex
         + " Final Position: " + FinalPosition);
        
        return m_FloorNodes[FinalIndex] ;
    }
    
    public FloorNode GetNode(int aRow, int aColumn)
    {
        return m_FloorNodes[m_FloorCore.GetIndex( aRow,aColumn)] ;
    }
    
    public FloorNode GetNode(Vector2Int aPosition)
    {
        return m_FloorNodes[m_FloorCore.GetIndex( aPosition.x,aPosition.y)] ;
    }
    

    public void SpawnNode(int aRow, int aColumn,int aIndex)
    {

        FloorNode tempFloorNode = Instantiate(m_FloorNodePrefab,m_Node.transform);
         
        m_FloorNodes[aIndex] =  tempFloorNode;

        m_FloorNodes[aIndex].m_PositionInGrid = new Vector2Int(aRow,aColumn);
        m_FloorNodes[aIndex].gameObject.name  = aRow + " " + aColumn;
        m_FloorNodes[aIndex].transform.position = new Vector3(4 * aRow, 0.5f, 4 * aColumn);
        m_FloorNodes[aIndex].m_NodeFloorManager = this;

        //  m_GridPathArray[x, y].m_Grid = m_Grid;
    }
}

