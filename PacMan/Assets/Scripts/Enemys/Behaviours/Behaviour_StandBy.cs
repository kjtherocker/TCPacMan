using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_StandBy : Behaviour
{
    public override void ActivateBehaviour()
    {
        m_CurrentTimer = 0;
        
        m_Ghost.SetGhostMaterial(m_Ghost.m_DefaultGhostMaterial);
        m_Ghost.SetCurrentFloorNode(m_Ghost.m_SpawnNode);
    }

    public override void UpdateBehaviour()
    {
        m_CurrentTimer += Time.deltaTime;

        if (m_CurrentTimer >= m_TimerEnd)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.Corner,true);
        }

    }
}
