using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Clyde : Behaviour
{
    // Start is called before the first frame update

    private int m_DistanceFromPacman = 4;
    private float m_FearTimer = 2;
    private float m_CurrentFearTimer;
    public override void Initialize(Ghosts aGhost , PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aGhost,aPacman,aFloorManager);
        
        m_CalculationRefreshRate = Helpers.Constants.GhostFrameRecalculation;
        m_GhostSpeed = Helpers.Constants.GhostNormalSpeed;
        m_TimerEnd = Helpers.Constants.GhostAggressiveTime;
    }


    public Vector2Int RandomNode()
    {
        return m_FloorManager.GetRandomNode().m_PositionInGrid;
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
        m_CurrentFearTimer += Time.deltaTime;
        
        
        if (m_CurrentTimer >= m_TimerEnd)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.Agression, true);
        }

        if (m_CurrentFearTimer >= m_FearTimer)
        {
            if (Vector3.Distance(m_Ghost.gameObject.transform.position, m_Pacman.transform.position) < 15.0f)
            {
                Debug.Log("RUN AWAY");
                m_GoalPosition = RandomNode();
                m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
                m_CurrentFearTimer = 0;
            }
        }

        if (m_Paths.Count > 0)
        {
            if (m_Paths[0] != null)
            {
                DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed);
            }
        }
    }
    
    public void Reset()
    {
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        m_CurrentTimer = 0;
        NextMove();
    }

    public override void NextMove()
    {

        if (m_Paths.Count <= 1)
        {
            Reset();
            return;
        }
        

        m_Paths.RemoveAt(0);

    }

}
