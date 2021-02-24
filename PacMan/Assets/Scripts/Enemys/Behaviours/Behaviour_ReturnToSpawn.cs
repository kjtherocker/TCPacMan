using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_ReturnToSpawn : Behaviour
{
    public override void Initialize(Ghosts aGhost , PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aGhost,aPacman,aFloorManager);

        m_GhostSpeed = 15.5f;
        m_TimerEnd = 10;
        m_GhostMaterial = Resources.Load<Material>("Ghost/Material_GhostEyes");
    }
    
    public override void ActivateBehaviour()
    {
        m_GoalPosition = m_Ghost.m_SpawnNode.m_PositionInGrid;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        m_Ghost.SetGhostMaterial(m_GhostMaterial);

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

    public override void PacmanContact()
    {
        
    }
}
