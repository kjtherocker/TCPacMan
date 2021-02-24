using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Ghost_Blinky : Ghosts
{
    // Start is called before the first frame update

    
    public override void Initialize(PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aPacman ,aFloorManager );
        m_GhostBehaviours.Add(GhostStates.Standby, new Behaviour_StandBy());
        m_GhostBehaviours.Add(GhostStates.GhostUnique, new Behaviour_Blinky());
        m_GhostBehaviours.Add(GhostStates.Agression, new Behaviour_Aggression());
        m_GhostBehaviours.Add(GhostStates.Corner, new Behaviour_Corner());
        m_GhostBehaviours.Add(GhostStates.Eatable, new Behaviour_Eatable());
        m_GhostBehaviours.Add(GhostStates.Death, new Behaviour_ReturnToSpawn());
        
        foreach (var behaviour in m_GhostBehaviours)
        {
            behaviour.Value.Initialize(this, m_Pacman , m_FloorManager);
        }

        m_DefaultGhostMaterial = Resources.Load<Material>("Ghost/Material_Blinky");
        
        m_GhostType = GhostTypes.Blinky;
        
    }
}
