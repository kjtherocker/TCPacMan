using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Corner : Behaviour
{
    public override void Initialize()
    {
        m_CalculationRefreshRate = 2;
        m_GhostSpeed = 6.5f;
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentPosition;
    }

    public override void UpdateBehaviour()
    {
        StartMoving();
    }
    
    public override void StartMoving()
    {
        m_GoalPosition = m_Ghost.m_CornerPosition;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        NextMove();
    }
    

    public override void NextMove()
    {

        
        m_Paths.RemoveAt(0);
        
        if (m_Paths.Count <= 0)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.GhostUnique);
            m_Ghost.ActivateGhostBehaviour();
            return;
        }
        m_Ghost.ProcessBehaviour(DirectMovement(m_Ghost.transform, m_Paths[0], m_GhostSpeed));
        
        
    }
}
