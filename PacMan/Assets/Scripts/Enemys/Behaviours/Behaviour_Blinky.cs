using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Behaviour_Blinky : Behaviour
{
    // Start is called before the first frame update
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
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        NextMove();
    }
    

    public override void NextMove()
    {
        if (m_Paths.Count <= 1)
        {
            StartMoving();
            return;
        }

        if (m_CurrentRefeshPosition >= m_CalculationRefreshRate)
        {
            m_GoalPosition = m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;
            m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
            m_CurrentRefeshPosition = 0;
        }
        else
        {
            m_CurrentRefeshPosition++;
        }
        
        
        m_Ghost.ProcessBehaviour(DirectMovement(m_Ghost.transform, m_Paths[1], m_GhostSpeed));
        m_Paths.RemoveAt(0);
        
    }


}
