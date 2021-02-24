using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Eatable : Behaviour
{
    public override void Initialize()
    {
        m_GhostSpeed = 0.5f;
        m_TimerEnd = 10;
        m_GhostMaterial = Resources.Load<Material>("Ghost/Material_EatableGhost");
    }
    
    public override void ActivateBehaviour()
    {
        m_GoalPosition = m_Ghost.m_CornerPosition;
        m_CurrentTimer = 0;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        m_Ghost.SetGhostMaterial(m_GhostMaterial);

        NextMove();
    }
    public override void UpdateBehaviour()
    {
        m_CurrentTimer += Time.deltaTime;

        if (m_CurrentTimer >= m_TimerEnd)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.GhostUnique,true);
        }
        
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

    public override void PacmanContact()
    {
        
    }


}
