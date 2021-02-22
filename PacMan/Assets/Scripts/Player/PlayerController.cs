﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 // Use this for initialization

    [SerializeField]
    
   
    public Floor.FloorDirections[] m_CardinalDirections;
    public Floor.FloorDirections m_CurrentDirection;
    public Floor.FloorDirections m_NextDirection;
    public int m_CurrentDirectionValue;

    public FloorNode m_CurrentNode;
    public FloorNode m_PreviousNode;
    
    public Vector2Int m_CurrentPosition;
    
    
    private bool m_IsRotating;
    private bool m_IsMoving;
    
    public FloorManager m_CurrentFloorManager;
    private Dictionary<Floor.FloorDirections, Vector3> m_DirectionRotations;

    private PlayerInput m_MovementControls;

    public void Initialize ()
    {
        m_CardinalDirections = new []
        {
            Floor.FloorDirections.Up, Floor.FloorDirections.Left, 
            Floor.FloorDirections.Down,Floor.FloorDirections.Right
        };
        

   
        m_CurrentDirectionValue = 0;
        m_CurrentDirection = m_CardinalDirections[m_CurrentDirectionValue];

        m_MovementControls = new PlayerInput();
        m_MovementControls.Enable();
        m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());
        m_MovementControls.Player.XButton.performed += xButton => Testo();


    }

    

    
    public  IEnumerator DirectMovement(Transform aObject, FloorNode  aTargetNode, float aTimeUntilDone)
    {
        Vector3 NewNodePosition = new Vector3(aTargetNode.transform.position.x,aTargetNode.transform.position.y + 2,
            aTargetNode.transform.position.z);

        float timeTaken = 0.0f;
        m_IsMoving = true;
        
        while (aTimeUntilDone - timeTaken > 0)
        {
            if (Vector3.Distance(aObject.transform.position, NewNodePosition) < 0.05f)
            {
                timeTaken = aTimeUntilDone;
            }

            timeTaken += Time.deltaTime;
            aObject.position = Vector3.Lerp(aObject.position, NewNodePosition, timeTaken /aTimeUntilDone );
            yield return null;
        }

        aObject.position = NewNodePosition;
        m_CurrentNode = aTargetNode;
        m_IsMoving = false;
        yield return 0;
    }



    public void Testo()
    {
        Debug.Log("TEEESSSto");
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


        MovetoNode(m_NextDirection);


    }

    public void MovetoNode(Floor.FloorDirections aDirection)
    {
        if (m_CurrentNode.IsDirectionWalkable(aDirection))
        {
            FloorNode TargetNode = m_CurrentFloorManager.GetNode(m_CurrentNode.m_PositionInGrid, aDirection);
        
            if (TargetNode == null)
            {
                Debug.Log("Cant Find Node " + m_CurrentNode.m_PositionInGrid);
                return;
            }
        
            StartCoroutine(DirectMovement(transform, TargetNode, 0.6f));
        }
    }
    
}

