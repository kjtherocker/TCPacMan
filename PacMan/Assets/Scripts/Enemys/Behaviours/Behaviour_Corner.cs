using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Corner : Behaviour
{
    public override void Initialize(Ghosts aGhost , PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aGhost,aPacman,aFloorManager);

        m_GhostSpeed = 9.5f;
    }
    
    public override void ActivateBehaviour()
    {
        m_GoalPosition = m_Ghost.m_CornerPosition;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        m_Ghost.SetGhostMaterial(m_Ghost.m_DefaultGhostMaterial);
        NextMove();
    }
    public override void UpdateBehaviour()
    {
        if (m_Paths[0] != null && m_Paths.Count > 0)
        {
            DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed);
        }
    }

    public override void NextMove()
    {
        m_Paths.RemoveAt(0);
        
        if (m_Paths.Count <= 0)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.GhostUnique,true);
            return;
        }
    }
}
