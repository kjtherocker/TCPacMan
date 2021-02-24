using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Inky : Behaviour
{
    // Start is called before the first frame update
    

    private Ghosts m_Blinky;
    public override void Initialize(Ghosts aGhost , PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aGhost,aPacman,aFloorManager);
        
        m_CalculationRefreshRate = 1;
        m_GhostSpeed = 10.5f;
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentPosition;
        m_TimerEnd = 10;
    }


    public Vector2Int CalculateBlinkyDistanceFromPacman()
    {
        
        Vector2Int Difference = m_Blinky.m_CurrentNode.m_PositionInGrid - m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;


        FloorNode currentNodeDifference = m_FloorManager.GetNode(m_Pacman.m_CurrentPosition + Difference * 2);
        
        if (currentNodeDifference == null)
        {
            return m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;
        }
        else
        {
            return m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid + Difference;   
        }
    }

    public override void ActivateBehaviour()
    {
        m_Blinky = GameManager.instance.GetGhost(Ghosts.GhostTypes.Blinky);
        
        m_GoalPosition = CalculateBlinkyDistanceFromPacman();
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
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.Agression, true);
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
            m_GoalPosition = CalculateBlinkyDistanceFromPacman();
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
