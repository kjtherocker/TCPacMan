using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]

public class FloorNode : Cell
{

    public delegate void WalkOnTopActivation();

    public WalkOnTopActivation m_WalkOnTopDelegate;
    

    

    public int m_Heuristic;
    public Vector2Int m_PositionInGrid;

    public int m_MovementCost;
    public int m_NodeHeight;
    public float m_NodeHeightOffset;
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    
    

    List<FloorNode> neighbours = null;
    public Grid m_Grid;




    public List<Floor.FloorDirections> m_WalkableDirections;
    
    public List<Floor.FloorDirections> m_InteractableDirections;

    public List<GameObject> NodeWalls;
    
    public FloorManager m_NodeFloorManager;


    // Use this for initialization



    public void Initialize(short aWalkableDirections)
    {
        m_MovementCost = 1;
        SetWalkableDirections(aWalkableDirections);
    }

    public void SetWalkOnTopDelegate(WalkOnTopActivation aWalkOnTopFunction)
    {
        m_WalkOnTopDelegate = aWalkOnTopFunction;
    }

    public void SetLevelNode(List<Floor.FloorDirections> aWalkableDirections)
    {
        for(int i = NodeWalls.Count; i < 0;i++)
        {
            NodeWalls[i].SetActive(true);
        }

        m_WalkableDirections = aWalkableDirections;
        
        foreach(Floor.FloorDirections node in m_WalkableDirections)
        {
            NodeWalls[(int)node].SetActive(false);
        }

    }
    
   public bool IsDirectionWalkable(Floor.FloorDirections aDirection)
   {
     for (int i = 0; i < m_WalkableDirections.Count; i++)
     {
         if (m_WalkableDirections[i] == aDirection)
         {
             return true;
         }
     }

       return false;
   }

   public void SetWalkableDirections(short aWalkabledirections)
   {
       if (m_WalkableDirections.Count != 0)
       {
           for (int i = m_WalkableDirections.Count; i > 0; i--)
           {
               m_WalkableDirections.RemoveAt(i);
           }
       }

       switch (aWalkabledirections)
       {
           case (short)Floor.FloorDirections.Up:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               break;
           
           case (short)Floor.FloorDirections.Left:
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               break;
           
           case (short)Floor.FloorDirections.Right:
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               break;
           
           case (short)Floor.FloorDirections.Down:
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               break;
           
           case (short)Floor.FloorDirections.AllSidesOpen:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               break;
           
           case (short)Floor.FloorDirections.UpDown:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               break;
           
           case (short)Floor.FloorDirections.UpLeft:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               break;
           
           case (short)Floor.FloorDirections.Upright:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               break;
           
           case (short)Floor.FloorDirections.LeftDown:
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               break;
           
           case (short)Floor.FloorDirections.RightDown:
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               break;
           
           case (short)Floor.FloorDirections.LeftRight:
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               break;
           
           case (short) Floor.FloorDirections.UpLeftRight:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               break;
           
           case (short)Floor.FloorDirections.UpLeftDown:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               m_WalkableDirections.Add(Floor.FloorDirections.Left);
               break;
           
           case (short)Floor.FloorDirections.UpRightDown:
               m_WalkableDirections.Add(Floor.FloorDirections.Up);
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               break;
           
           case (short)Floor.FloorDirections.DownLeftRight:
               m_WalkableDirections.Add(Floor.FloorDirections.Left); 
               m_WalkableDirections.Add(Floor.FloorDirections.Down);
               m_WalkableDirections.Add(Floor.FloorDirections.Right);
               break;

           
           
       }
       
       SetLevelNode(m_WalkableDirections);
   }


   protected static readonly Vector2[] _directions =
    {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1)
    };
    
    public override List<FloorNode> GetNeighbours(List<FloorNode> cells)
    {
        if (neighbours == null)
        {
            neighbours = new List<FloorNode>(4);
            foreach (var direction in _directions)
            {
                var neighbour = cells.Find(c => c.m_PositionInGrid == m_PositionInGrid + direction);
                if (neighbour == null) continue;

                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

}



