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

    protected Behaviour CurrentBehaviour;

    public FloorNode m_CurrentNode;
    public GhostTypes m_GhostType;

    private PlayerController m_Pacman;

    private List<NodeInfo> m_OpenList;
    private NodeInfo[] m_FloorCopy;
    
    public virtual void Initialize()
    {
        m_GhostBehaviours = new Dictionary<GhostStates, Behaviour>();
        m_OpenList = new List<NodeInfo>();
        m_FloorCopy = new NodeInfo[20*20];
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

        HeuristicTesto(m_FloorCopy , m_Pacman.m_CurrentNode.m_PositionInGrid);
    }


    public void HeuristicTesto(NodeInfo[] aFloorCopy, Vector2Int aStart)
    {
        int Spawnindex = GameManager.instance.m_FloorManager.m_FloorCore.GetIndex(aStart.x, aStart.y);
        int m_CostSoFar = 0;
        
        aFloorCopy[Spawnindex].m_IsGoal = true;
        aFloorCopy[Spawnindex].m_HeuristicCalculated = true;
        aFloorCopy[Spawnindex].m_MovementCost = 0;

        m_OpenList.Add(aFloorCopy[Spawnindex]);



        while (m_OpenList.Count > 0)
        {
            AddNeighborsHeuristic(m_OpenList[0].m_PositionInGrid);
            m_OpenList.RemoveAt(0);
        }





    }

    public void GetLowestHeuristic()
    {
        
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
        CurrentBehaviour = m_GhostBehaviours[aGhostState];
    }



    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ChangeLives(-1);
        
    }
}
