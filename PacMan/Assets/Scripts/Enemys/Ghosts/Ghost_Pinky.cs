using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Pinky : Ghosts
{
    public override void Initialize()
    {
        base.Initialize();
        m_GhostBehaviours.Add(GhostStates.GhostUnique, new Behaviour_Pinky());
        m_GhostBehaviours.Add(GhostStates.Agression, new Behaviour_Aggression());
        m_GhostBehaviours.Add(GhostStates.Corner, new Behaviour_Corner());

        m_GhostType = GhostTypes.Pinky;
        
        SetGhostBehaviour(GhostStates.Corner);
    }
}
