using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions.Must;
using static UnityEditor.PrefabUtility;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
[System.Serializable]
public class FloorManager : MonoBehaviour
{

    
    public enum FloorGimmicks
    {
        Empty = 0,
        Pellet = 1,
        Ghostbuster = 2,
    }
    

    public Floor m_FloorCore;
    public FloorNode[] m_FloorNodes;
    public FloorNode m_FloorNodePrefab;
    private Dictionary<Floor.FloorDirections, Vector2Int>  m_CardinalPositions;
    public NodeInfo[] m_NodeInfoArray {get;  private set;}
    
    
    //Empty Game Objects to spawn under
    public GameObject m_Node;
    public GameObject m_Gimmicks;
    
    
    private GameObject m_Pellet;
    private GameObject m_Ghostbuster;
    private PlayerController m_PacMan;
    
    //ObjectPools
    public List<GameObject> m_PelletPool {get;  private set;}
    public List<GameObject> m_GhostBusterPool {get;  private set;}
    
    public void Initialize(PlayerController aPacman)
    {
        m_PacMan = aPacman;
        
        InitializeCardinalPositions();
        SpawnPacman();

        m_FloorCore.SpawnGhosts();
        m_Pellet = Resources.Load<GameObject>("Gimmicks/Prefab_Pellet");
        m_Ghostbuster = Resources.Load<GameObject>("Gimmicks/Prefab_GhostBuster");
    }


    public void InitializeCardinalPositions()
    {
        if (m_CardinalPositions != null)
        {
            return;
        }

        m_CardinalPositions = new Dictionary<Floor.FloorDirections, Vector2Int>();
        m_CardinalPositions.Add(Floor.FloorDirections.Up, new Vector2Int(-1,0));
        m_CardinalPositions.Add(Floor.FloorDirections.Down, new Vector2Int(1,0));
        m_CardinalPositions.Add(Floor.FloorDirections.Left, new Vector2Int(0,-1));
        m_CardinalPositions.Add(Floor.FloorDirections.Right, new Vector2Int(0,1));
    }

    public void SpawnNodeInfo()
    {
        m_FloorCore.GhostDoorOffset();

        m_NodeInfoArray = new NodeInfo[m_FloorCore.m_GridDimensionX * m_FloorCore.m_GridDimensionY];
        for (int i = 0; i < m_NodeInfoArray.Length; i++)
        {
            if (m_FloorNodes[i] == null)
            {
                continue;
            }

            m_NodeInfoArray[i] = m_FloorNodes[i].GetNodeInfo();
        }



        AddAdditionalNeighbors(m_FloorCore.m_GhostDoorPosition, m_FloorCore.m_GhostDoorNeightbor);

    }

    public void AddAdditionalNeighbors(Vector2Int aInitialObject, Vector2Int aNeightbor)
    {
        FloorNode ghostDoorNeightborOffset = GetNode(aNeightbor);
        int ghostDoorIndex = m_FloorCore.GetIndex(aInitialObject);
        m_NodeInfoArray[ghostDoorIndex].m_Neighbours.Add(ghostDoorNeightborOffset);
        
    }



    public void SpawnGhosts(Ghosts[] aGhosts)
    {
        for (int i = 0; i < aGhosts.Length; i++)
        {

            FloorNode tempFloorNode = GetNode(m_FloorCore.m_GhostSpawnPositions[aGhosts[i].m_GhostType]);
            aGhosts[i].SetCurrentFloorNode(tempFloorNode);
            aGhosts[i].SetCornerPosition(m_FloorCore.m_GhostCornerPositions[aGhosts[i].m_GhostType]);
            aGhosts[i].m_SpawnNode = tempFloorNode;
        }
    }




    public void CreateGrid()
    {
        m_FloorCore.Initialize();

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


        m_FloorNodes = new FloorNode[m_FloorCore.m_GridDimensionX * m_FloorCore.m_GridDimensionY];
        SetLevelNodes(m_FloorCore.m_FloorBlueprint);
        
        SetLevelNodesNeightbors();

    }

    public void SetLevelNodesNeightbors()
    {
        for (int x = 0; x < m_FloorCore.m_GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.m_GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (m_FloorNodes[LevelIndex] == null)
                {
                    continue;
                }
            }
        }
    }


    public void SetLevelNodes(short[] aLevelBlueprint)
    {
        for (int x = 0; x < m_FloorCore.m_GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.m_GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (aLevelBlueprint[LevelIndex] == (short) Floor.FloorDirections.Empty)
                {
                    continue;
                }

                SpawnNode(x , y,LevelIndex );
                
                m_FloorNodes[LevelIndex].Initialize(aLevelBlueprint[LevelIndex], this);
            }
        }
    }

    public void GenerateGimmickPools()
    {
        
        m_PelletPool = new List<GameObject>();
        m_GhostBusterPool = new List<GameObject>();
        
        for (int x = 0; x < m_FloorCore.m_GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.m_GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (m_FloorCore.m_GoalsBlueprint[LevelIndex] == (short) FloorGimmicks.Empty)
                {
                    continue;
                }
                
                CreateGimmickPools( m_FloorCore.m_GoalsBlueprint[LevelIndex]);

            }
        }
    }
    
    public void CreateGimmickPools( short aGimmickType)
    {
        GameObject gimmickToSpawn = null;
        Vector3 nodeOffset = new Vector3(0,2,0);
        switch (aGimmickType)
        {
            case (short)FloorGimmicks.Pellet:
                gimmickToSpawn = Instantiate(m_Pellet,m_Gimmicks.transform);
                gimmickToSpawn.SetActive(false);
                m_PelletPool.Add(gimmickToSpawn);
                
                break;
            
            case (short)FloorGimmicks.Ghostbuster:
                gimmickToSpawn = Instantiate(m_Ghostbuster,m_Gimmicks.transform);
                gimmickToSpawn.SetActive(false);
                m_GhostBusterPool.Add(gimmickToSpawn);
                
                break;
            
  
            
        }
    }
    
    public void SpawnGimmickPools()
    {
        TurnObjectsInPoolOff( m_PelletPool);
        TurnObjectsInPoolOff( m_GhostBusterPool);
        
        for (int x = 0; x < m_FloorCore.m_GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.m_GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (m_FloorCore.m_GoalsBlueprint[LevelIndex] == (short) FloorGimmicks.Empty)
                {
                    continue;
                }
                
                RespawnGimmicks();

            }
        }
    }

    public void TurnObjectsInPoolOff( List<GameObject> aList)
    {
        for (int i = 0; i < aList.Count; i++)
        {
            aList[i].SetActive(false);
        }

    }


    public void RespawnGimmicks()
    {


        for (int x = 0; x < m_FloorCore.m_GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.m_GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (m_FloorCore.m_GoalsBlueprint[LevelIndex] == (short) FloorGimmicks.Empty)
                {
                    continue;
                }
                
                SpawnGimmick(m_FloorNodes[LevelIndex], m_FloorCore.m_GoalsBlueprint[LevelIndex]);

            }
        }
    }
    
    public void SpawnGimmick(FloorNode aFloornode, short aGimmickType)
    {
        GameObject gimmickToSpawn = null;
        switch (aGimmickType)
        {
            case (short)FloorGimmicks.Pellet:
                gimmickToSpawn = GetInActiveGimmickObject( m_PelletPool);
                if (gimmickToSpawn == null)
                {
                    return;
                }

                gimmickToSpawn.transform.position = aFloornode.transform.position +  Helpers.Constants.HeightOffGrid;;

                break;
            
            case (short)FloorGimmicks.Ghostbuster:
                gimmickToSpawn = GetInActiveGimmickObject( m_GhostBusterPool);
                if (gimmickToSpawn == null)
                {
                    return;
                }
                
                gimmickToSpawn.transform.position = aFloornode.transform.position + Helpers.Constants.HeightOffGrid;;
                
                
                break;
        }
        

    }
    





    public GameObject GetInActiveGimmickObject( List<GameObject> aGimmick)
    {
        for (int i = 0; i < aGimmick.Count; i++)
        {
            if (aGimmick[i].gameObject.activeInHierarchy == false)
            {
                aGimmick[i].SetActive(true);
               return aGimmick[i];
            }
        }

  
        return null;

    }
    

    public void SpawnPacman()
    {
        int LevelIndex = m_FloorCore.GetIndex(
            m_FloorCore.m_DefaultSpawnPosition.x, m_FloorCore.m_DefaultSpawnPosition.y);

        Vector3 m_NodePosition = m_FloorNodes[LevelIndex].gameObject.transform.position;
        Vector3 m_PositionOffset =  Helpers.Constants.HeightOffGrid;

        m_PacMan.m_CurrentNode = m_FloorNodes[LevelIndex];
        m_PacMan.m_CurrentPosition = m_FloorCore.m_DefaultSpawnPosition;

        m_PacMan.m_StartNode = m_FloorNodes[LevelIndex];
        m_PacMan.m_SpawnPosition = m_NodePosition + m_PositionOffset;
        m_PacMan.transform.position = m_NodePosition + m_PositionOffset;
        m_PacMan.Initialize();
    }




    public FloorNode GetNode(Vector2Int CurrentPosition,Floor.FloorDirections TargetDirection)
    {
        InitializeCardinalPositions();
        Vector2Int FinalPosition = new Vector2Int(CurrentPosition.x + m_CardinalPositions[TargetDirection].x,
            CurrentPosition.y + m_CardinalPositions[TargetDirection].y );
        
        int FinalIndex = m_FloorCore.GetIndex( FinalPosition.x,FinalPosition.y) ;
        
        return m_FloorNodes[FinalIndex] ;
    }

    public FloorNode GetRandomNode()
    {
        int row = Random.Range(0, m_FloorCore.m_GridDimensionX);
        int column = Random.Range(0, m_FloorCore.m_GridDimensionY);

        FloorNode randomNode = m_FloorNodes[m_FloorCore.GetIndex(row, column)];

        while (randomNode == null)
        {
            row = Random.Range(0, m_FloorCore.m_GridDimensionX);
            column = Random.Range(0, m_FloorCore.m_GridDimensionY);
            randomNode = m_FloorNodes[m_FloorCore.GetIndex(row, column)];
        }


        return randomNode ;
    }

    public FloorNode GetNode(int aRow, int aColumn)
    {
        if (aRow > m_FloorCore.m_GridDimensionX || aColumn > m_FloorCore.m_GridDimensionY)
        {
            return m_FloorNodes[m_FloorCore.GetIndex(aRow, aColumn)];
        }

        return null;
    }
    
    public FloorNode GetNode(Vector2Int aPosition)
    {
        if (aPosition.x >= m_FloorCore.m_GridDimensionX || aPosition.y >= m_FloorCore.m_GridDimensionY ||
            aPosition.x < 0 || aPosition.y < 0)
        {
            return null;
        }

        return m_FloorNodes[m_FloorCore.GetIndex(aPosition.x, aPosition.y)];
    }
    

    public void SpawnNode(int aRow, int aColumn,int aIndex)
    {

        FloorNode tempFloorNode = Instantiate(m_FloorNodePrefab,m_Node.transform);
         
        m_FloorNodes[aIndex] =  tempFloorNode;

        m_FloorNodes[aIndex].m_PositionInGrid = new Vector2Int(aRow,aColumn);
        m_FloorNodes[aIndex].gameObject.name  = aRow + " " + aColumn;
        m_FloorNodes[aIndex].transform.position = new Vector3(4 * aRow, 0.5f, 4 * aColumn);
        m_FloorNodes[aIndex].m_NodeFloorManager = this;
        
    }
}

