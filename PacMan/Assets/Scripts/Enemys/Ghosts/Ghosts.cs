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

    public virtual void Initialize()
    {
        m_GhostBehaviours = new Dictionary<GhostStates, Behaviour>();
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
