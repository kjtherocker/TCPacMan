using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Clyde : Ghosts
{
    public override void Initialize(PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aPacman ,aFloorManager );
        m_GhostBehaviours.Add(GhostStates.Standby, new Behaviour_StandBy());
        m_GhostBehaviours.Add(GhostStates.GhostUnique, new  Behaviour_Blinky());
        m_GhostBehaviours.Add(GhostStates.Agression, new Behaviour_Blinky());
        m_GhostBehaviours.Add(GhostStates.Corner, new Behaviour_Corner());
        m_GhostBehaviours.Add(GhostStates.Eatable, new Behaviour_Eatable());
        m_GhostBehaviours.Add(GhostStates.Death, new Behaviour_ReturnToSpawn());
        
        foreach (var behaviour in m_GhostBehaviours)
        {
            behaviour.Value.Initialize(this, m_Pacman , m_FloorManager);
        }

        m_DefaultGhostMaterial = Resources.Load<Material>("Ghost/Material_Clyde");

        m_GhostType = GhostTypes.Cylde;

    }
}
