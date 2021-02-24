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
        m_GhostSpeed = 10.5f;
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentPosition;
        m_TimerEnd = 10;
    }
    
    public override void ActivateBehaviour()
    {
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        m_Ghost.SetGhostMaterial(m_Ghost.m_DefaultGhostMaterial);
        m_CurrentTimer = 0;
        NextMove();
    }

    public override void UpdateBehaviour()
    {
        m_CurrentTimer += Time.deltaTime;

        if (m_CurrentTimer >= m_TimerEnd)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.Corner,true);
        }

        if (m_Paths.Count > 0)
        {
            if (m_Paths[0] != null)
            {
                DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed);
            }
        }
    }
    
    public override void NextMove()
    {
        
        if (m_Paths.Count <= 1)
        {
            ActivateBehaviour();
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
        
        m_Paths.RemoveAt(0);
        
    }


}
