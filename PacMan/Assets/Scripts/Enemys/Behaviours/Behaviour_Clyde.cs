using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Clyde : Behaviour
{
    // Start is called before the first frame update

    private int m_DistanceFromPacman = 4;
    private float m_FearTimer = 2;
    private float m_CurrentFearTimer;
    public override void Initialize()
    {

        m_GhostSpeed = 10.5f;
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentPosition;
        m_TimerEnd = 10;
    }


    public Vector2Int RandomNode()
    {
        return GameManager.instance.m_FloorManager.GetRandomNode().m_PositionInGrid;
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
            if (Vector3.Distance(m_Ghost.gameObject.transform.position, m_Ghost.m_Pacman.transform.position) < 15.0f)
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

    public override void NextMove()
    {

        if (m_Paths.Count <= 1)
        {
            ActivateBehaviour();
            return;
        }
        

        m_Paths.RemoveAt(0);

    }

}
