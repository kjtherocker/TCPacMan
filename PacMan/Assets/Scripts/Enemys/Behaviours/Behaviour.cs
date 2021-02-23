using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour
{

    private Ghosts m_Ghost;
    private List<FloorNode> m_Paths;
    public void SetGhost(Ghosts aGhost)
    {
        m_Ghost = aGhost;
    }
    
    public void SetGoal(Vector2Int m_Goal)
    {
      //  m_Grid.SetHeuristicToZero();
      //  m_Grid.GetNode(m_Goal.x, m_Goal.y).m_IsGoal = true;
    }

    public virtual void SetGoalPosition(Vector2Int m_Goal)
    {

        SetGoal(m_Goal);

    // m_NodeInWalkableRange =
    //     GetAvailableDestinations(m_Grid.m_GridPathList, m_Grid.GetNode(m_Goal.x, m_Goal.y), 100);
    // 
    // foreach (CombatNode node in m_NodeInWalkableRange)
    // {
    //     node.m_IsWalkable = true;
    // }

    // List<FloorNode> TempList = m_Grid.GetTheLowestH(Node_ObjectIsOn.m_PositionInGrid, m_Movement);


    // StartCoroutine(GetToGoal(TempList));

    }
    
    
    
   // public Dictionary<FloorNode, List<FloorNode>> findAllPaths(Dictionary<FloorNode, Dictionary<FloorNode, int>> edges, FloorNode originNode,int m_Range)
   // {
   //     IPriorityQueue<FloorNode> frontier = new HeapPriorityQueue<FloorNode>();
   //     frontier.Enqueue(originNode, 0);
//
   //     Dictionary<FloorNode, FloorNode> cameFrom = new Dictionary<FloorNode, FloorNode>();
   //     cameFrom.Add(originNode, default(FloorNode));
   //     Dictionary<FloorNode, int> costSoFar = new Dictionary<FloorNode, int>();
   //     costSoFar.Add(originNode, 0);
   //     while (frontier.Count != 0)
   //     {
   //         var current = frontier.Dequeue();
   //         List<FloorNode>neighbours = GetNeigbours(edges, current);
   //         foreach (FloorNode neighbour in neighbours)
   //         {
   //             int newCost = costSoFar[current] + edges[current][neighbour];
   //             if (!costSoFar.ContainsKey(neighbour) || newCost < costSoFar[neighbour])
   //             {
   //                 if (newCost > m_Range)
   //                 {
   //                     break;
   //                 }
   //                 
//
   //                 costSoFar[neighbour] = newCost;
   //                 cameFrom[neighbour] = current;
   //                 frontier.Enqueue(neighbour, newCost);
   //             }
   //         }
   //     }
   //     
   //     
   //     
   //     //Setting the destination
   //     Dictionary<FloorNode, List<FloorNode>> paths = new Dictionary<FloorNode, List<FloorNode>>();
   //     foreach (FloorNode destination in cameFrom.Keys)
   //     {
   //         List<FloorNode> path = new List<FloorNode>();
   //         var current = destination;
   //         while (!current.Equals(originNode))
   //         {
   //             path.Add(current);
   //             current = cameFrom[current];
   //         }
   //         paths.Add(destination, path);
   //     }
   //     return paths;
   // }
    

    public void MoveTo()
    {

        //GameManager.instance.m_Pacman.m_CurrentNode;
        // m_Paths = CalculatePath();
        // m_Ghost.transform.position = m_Paths[0].transform.position + new Vector3(0,2,0);
    }

    public void MoveTowards()
    {
        
    }

    public List<FloorNode> CalculatePath()
    {

        List<FloorNode> FinalList = new List<FloorNode>();

      //  FinalList = m_Ghost.m_CurrentNode.GetNeighbours(GameManager.instance.m_FloorManager.m_FloorNodes);
        

        return FinalList;
    }
}
