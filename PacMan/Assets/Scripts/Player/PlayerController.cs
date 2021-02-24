using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 // Use this for initialization

    public enum PacmanStates
    {
        Start,
        Normal,
        Death
    }
    
    
    
   
    public Floor.FloorDirections[] m_CardinalDirections;
    public Floor.FloorDirections m_CurrentDirection;
    public Floor.FloorDirections m_NextDirection;
    public int m_CurrentDirectionValue;

   // public List<> m_DeathObservers;

    public Vector3 m_SpawnPosition;
    public FloorNode m_StartNode;
    public FloorNode m_CurrentNode;
    private FloorNode m_NextNode;
    
    public Vector2Int m_CurrentPosition;

    private PacmanStates m_PacmanState;
    
    private bool m_IsRotating;
    private bool m_IsMoving;
    
    public FloorManager m_CurrentFloorManager;
    private Dictionary<Floor.FloorDirections, Vector3> m_DirectionRotations;

    private PlayerInput m_MovementControls;

    private float m_PacManSpeed = 17;
    private float m_MiniumDistanceBetweenNodes = 0.05f;
    private float m_MaxDistanceForDirectionChange = 2.25f;
    
    public void Initialize ()
    {
        m_CardinalDirections = new []
        {
            Floor.FloorDirections.Up, Floor.FloorDirections.Down,
            Floor.FloorDirections.Left, Floor.FloorDirections.Right
        };
        

   
        m_CurrentDirectionValue = 0;
        m_CurrentDirection = m_CardinalDirections[(short)Floor.FloorDirections.Left -1];

        m_MovementControls = new PlayerInput();
        m_MovementControls.Enable();
        m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());
        m_PacmanState = PacmanStates.Start;
    }


    public void Update()
    {

        if (m_PacmanState == PacmanStates.Normal)
        {
            
            
            

            if (m_CurrentNode.IsDirectionWalkable(m_NextDirection))
            {
                //Maximum possible distance that we can make last minute changes to our direction
                if(Vector3.Distance(transform.position, m_CurrentNode.transform.position) < m_MaxDistanceForDirectionChange)
                {

                    MovetoNode(m_NextDirection);
                    return;
                }

            }

            if (m_CurrentNode.IsDirectionWalkable(m_CurrentDirection))
            {
                MovetoNode(m_CurrentDirection);
            }
        }

    }

    public void SetPacManState(PacmanStates aPacmanStates)
    {
        m_PacmanState = aPacmanStates;
    }

    
    public void Death()
    {
        Debug.Log("Pacman has been slain");
        ReturnToSpawn();
        GameManager.instance.ChangeLives(-1);
    }


    public void ReturnToSpawn()
    {
        transform.position = m_SpawnPosition;
        SetCurrentNode(m_StartNode);
    }

    public void SetCurrentNode(FloorNode aFloorNode)
    {
        m_CurrentNode = aFloorNode;
        m_CurrentPosition = aFloorNode.m_PositionInGrid;
    }


    public  void DirectMovement(Transform aObject, FloorNode  aTargetNode, float aTimeUntilDone)
    {
        Vector3 NewNodePosition = new Vector3(aTargetNode.transform.position.x,aTargetNode.transform.position.y + 2,
            aTargetNode.transform.position.z);

        float timeTaken = 0.0f;
        m_IsMoving = true;
        

        if (Vector3.Distance(aObject.transform.position, NewNodePosition) < m_MiniumDistanceBetweenNodes)
        {
            SetCurrentNode(aTargetNode);
            return;
        }
        
        aObject.position = Vector3.MoveTowards(aObject.position, NewNodePosition, m_PacManSpeed * Time.deltaTime);
        
      //  Vector3 relativePos = NewNodePosition - transform.position;
      // 
      //  Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
      //  transform.rotation = rotation;
    }

    



    public void PlayerMovement(Vector2 aDirection)
    {

        if (aDirection == new Vector2(-1,0))
        {
            m_NextDirection = Floor.FloorDirections.Left;
        }
        
        if (aDirection == new Vector2(1,0))
        {
            m_NextDirection = Floor.FloorDirections.Right;
        }
        
        if (aDirection == new Vector2(0,1))
        {
            m_NextDirection = Floor.FloorDirections.Up;
        }
        
        if (aDirection == new Vector2(0,-1))
        {
            m_NextDirection = Floor.FloorDirections.Down;
        }
        

    }

    public void MovetoNode(Floor.FloorDirections aDirection)
    {
        if (m_CurrentNode.IsDirectionWalkable(aDirection))
        {
            m_CurrentDirection = aDirection;
            FloorNode TargetNode = m_CurrentFloorManager.GetNode(m_CurrentNode.m_PositionInGrid, aDirection);
        
            if (TargetNode == null)
            {
                Debug.Log("Cant Find Node " + m_CurrentNode.m_PositionInGrid);
                return;
            }
        
            DirectMovement(transform, TargetNode, 4.6f);
        }
    }
    
}


