using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Ghost_Blinky : Ghosts
{
    // Start is called before the first frame update

    
    public override void Initialize()
    {
        base.Initialize();
        m_GhostBehaviours.Add(GhostStates.Standby, new Behaviour_StandBy());
        m_GhostBehaviours.Add(GhostStates.GhostUnique, new Behaviour_Blinky());
        m_GhostBehaviours.Add(GhostStates.Agression, new Behaviour_Aggression());
        m_GhostBehaviours.Add(GhostStates.Corner, new Behaviour_Corner());
        
        foreach (var behaviour in m_GhostBehaviours)
        {
            behaviour.Value.SetGhost(this);
            behaviour.Value.Initialize();
        }

        m_DefaultGhostMaterial = Resources.Load<Material>("Ghost/Material_Blinky");
        
        m_GhostType = GhostTypes.Blinky;
        
    }
}
