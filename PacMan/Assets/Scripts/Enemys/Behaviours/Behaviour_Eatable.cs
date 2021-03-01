using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Eatable : Behaviour
{
    public override void Initialize(Ghosts aGhost , PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aGhost,aPacman,aFloorManager);

        m_GhostSpeed = Helpers.Constants.GhostEatableSpeed;
        m_TimerEnd = Helpers.Constants.GhostEatbleTime;
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
        
        if (m_Paths.Count > 0)
        {
            if (m_Paths[0] != null)
            {
                DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed);
            }
        }
    }

    
    public void Reset()
    {
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentNode.m_PositionInGrid;
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        NextMove();
    }

    
    public override void NextMove()
    {
        m_Paths.RemoveAt(0);
        
        if (m_Paths.Count <= 0)
        {
            Reset();
            return;
        }
    }

    public override void PacmanContact()
    {
        AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.EatGhost, AudioManager.Soundtypes.SoundEffects);
        m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.Death,true);
    }


}
