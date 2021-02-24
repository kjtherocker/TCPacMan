using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Inky : Ghosts
{
    public override void Initialize()
    {
        base.Initialize();
        m_GhostBehaviours.Add(GhostStates.GhostUnique, new Behaviour_Blinky());
        m_GhostBehaviours.Add(GhostStates.Agression, new Behaviour_Aggression());
        m_GhostBehaviours.Add(GhostStates.Corner, new Behaviour_Corner());

        foreach (var behaviour in m_GhostBehaviours)
        {
            behaviour.Value.SetGhost(this);
            behaviour.Value.Initialize();
        }



        m_GhostType = GhostTypes.Inky;
        
        SetGhostBehaviour(GhostStates.Corner);
    }
}
