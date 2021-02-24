using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public enum GhostStates
    {
        GhostUnique,
        Agression,
        Corner,
        
    }

    public enum GhostTypes
    {
        Cylde,
        Inky,
        Blinky,
        Pinky
    }


    protected Dictionary<GhostStates, Behaviour> m_GhostBehaviours;

    protected Behaviour m_CurrentBehaviour;

    public FloorNode m_CurrentNode;
    public GhostTypes m_GhostType;

    public PlayerController m_Pacman;

    private List<NodeInfo> m_OpenList;
    public List<FloorNode> m_PathToFollow;
    private NodeInfo[] m_FloorCopy;
    public Vector2Int m_CornerPosition;

    
    public virtual void Initialize()
    {
        m_GhostBehaviours = new Dictionary<GhostStates, Behaviour>();
        m_FloorCopy = new NodeInfo[20*20];
        m_PathToFollow = new List<FloorNode>();
        m_OpenList = new List<NodeInfo>();
    }

    public void CopyFloor(NodeInfo[] aFloorCopy)
    {
        for (int i = 0; i < aFloorCopy.Length; i++)
        {
            if (aFloorCopy[i] == null)
            {
                continue;
            }

            m_FloorCopy[i] = new NodeInfo();
            m_FloorCopy[i].m_IsGoal = aFloorCopy[i].m_IsGoal;
            m_FloorCopy[i].m_MovementCost = aFloorCopy[i].m_MovementCost;
            m_FloorCopy[i].m_PositionInGrid = aFloorCopy[i].m_PositionInGrid;
            m_FloorCopy[i].m_Neighbours = aFloorCopy[i].m_Neighbours;
        }
        
    }


    public void ResetFloorArray()
    {
        for (int i = 0; i < m_FloorCopy.Length; i++)
        {
            if (m_FloorCopy[i] == null)
            {
                continue;
            }
            
            m_FloorCopy[i].m_IsGoal = false;
            m_FloorCopy[i].m_MovementCost = -1;
            m_FloorCopy[i].m_HeuristicCalculated = false;
        }

        for (int i = 0; i < m_OpenList.Count; i++)
        {
            m_OpenList.RemoveAt(0);
        }
        
        for (int i = 0; i < m_PathToFollow.Count; i++)
        {
            m_PathToFollow.RemoveAt(0);
        }

    }


    public void SetCornerPosition(Vector2Int aCornerPosition)
    {
        m_CornerPosition = aCornerPosition;
    }

    public void ProcessBehaviour(IEnumerator aCoroutine)
    {
        StartCoroutine(aCoroutine);
    }

    
    public List<FloorNode> CalculatePath( Vector2Int aStart)
    {
        ResetFloorArray();
        
        m_PathToFollow = new List<FloorNode>();
        m_OpenList = new List<NodeInfo>();
        
        //Calculating path Heuristic
        int Spawnindex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex(aStart.x, aStart.y);
        int m_CostSoFar = 0;
        
        m_FloorCopy[Spawnindex].m_IsGoal = true;
        m_FloorCopy[Spawnindex].m_HeuristicCalculated = true;
        m_FloorCopy[Spawnindex].m_MovementCost = 0;

        m_OpenList.Add(m_FloorCopy[Spawnindex]);

        bool isCalculatingHeuristic = true;
        
        while (isCalculatingHeuristic)
        {
            if (m_OpenList.Count <= 0)
            {
                isCalculatingHeuristic = false;
                break;
            }
            
            NodeInfo openNodeInfo = m_OpenList[0];

            if (openNodeInfo.m_PositionInGrid == m_CurrentNode.m_PositionInGrid)
            {
                isCalculatingHeuristic = false;
                break;
            }
            
            AddNeighborsHeuristic(openNodeInfo.m_PositionInGrid);
            m_OpenList.RemoveAt(0);
        }

        
        //Calculating the quckest path to goal
    

        FloorNode GetFloornode = GameManager.instance.m_FloorManager.GetNode(m_CurrentNode.m_PositionInGrid);

        int currentindex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex( m_CurrentNode.m_PositionInGrid);
        
        m_PathToFollow.Add(GetFloornode);

        while (m_PathToFollow.Count -1 < m_FloorCopy[currentindex].m_MovementCost)
        {
            int pathiIndex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex(m_PathToFollow[m_PathToFollow.Count -1].m_PositionInGrid);
            m_PathToFollow.Add(CheckNeighborsForLowestNumber(m_FloorCopy[pathiIndex]));
        }

        return m_PathToFollow;
    }

    


    public FloorNode CheckNeighborsForLowestNumber(NodeInfo aNodeInfo)
    {
        float TempHeuristic = 100;
        FloorNode TempNode = null;
        

        foreach (FloorNode floorNode in aNodeInfo.m_Neighbours)
        {

                int pathiIndex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex(floorNode.m_PositionInGrid);
                if (floorNode == null)
                {
                    Debug.Log("neighbour  doesnt exist");
                    continue;
                }

                if (m_FloorCopy[pathiIndex].m_MovementCost  == -1)
                {
                   // Debug.Log("neighbour heuristic is  -1");
                    continue;
                }

                if (m_FloorCopy[pathiIndex].m_MovementCost < TempHeuristic)
                {
                    TempHeuristic = m_FloorCopy[pathiIndex].m_MovementCost;
                    TempNode = floorNode;
                }
            
        }
        
        return TempNode;
    }
    

    public void AddNeighborsHeuristic(Vector2Int aPosition)
    {
        if (aPosition == m_CurrentNode.m_PositionInGrid)
        {
            return;
        }
        
        
        int Spawnindex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex(aPosition.x, aPosition.y);
        
         foreach (FloorNode floorNode in m_FloorCopy[Spawnindex].m_Neighbours)
         {
             int newindex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex(floorNode.m_PositionInGrid.x,
                 floorNode.m_PositionInGrid.y);
             if (m_FloorCopy[newindex].m_HeuristicCalculated)
             {
                 continue;
             }

             m_FloorCopy[newindex].m_MovementCost =  m_FloorCopy[Spawnindex].m_MovementCost + 1;
             m_FloorCopy[newindex].m_HeuristicCalculated = true;
             floorNode.THISISTESTREMOVE = m_FloorCopy[Spawnindex].m_MovementCost + 1;
             
             m_OpenList.Add(m_FloorCopy[newindex]);
             
         }
    }


    public void SetPacman(PlayerController aPlayerController)
    {
        m_Pacman = aPlayerController;

    }

    public virtual void SetCurrentFloorNode( FloorNode aFloorNode)
    {
        m_CurrentNode = aFloorNode;
        transform.position =
            m_CurrentNode.transform.position + new Vector3(0,2,0);
    }

    public virtual void SetGhostBehaviour(GhostStates aGhostState)
    {
        m_CurrentBehaviour = m_GhostBehaviours[aGhostState];
        
    }

    public void ActivateGhostBehaviour()
    {
        m_CurrentBehaviour.StartMoving();
    }



    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ChangeLives(-1);
        
    }
}
