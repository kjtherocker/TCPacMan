using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Clyde : Ghosts
{
    public override void Initialize()
    {
        base.Initialize();
        m_GhostBehaviours.Add(GhostStates.GhostUnique, new Behaviour_Clyde());
        m_GhostBehaviours.Add(GhostStates.Agression, new Behaviour_Aggression());
        m_GhostBehaviours.Add(GhostStates.Corner, new Behaviour_Corner());
        
        foreach (var behaviour in m_GhostBehaviours)
        {
            behaviour.Value.SetGhost(this);
        }

        m_GhostType = GhostTypes.Cylde;
        
        SetGhostBehaviour(GhostStates.Corner);
    }
}
