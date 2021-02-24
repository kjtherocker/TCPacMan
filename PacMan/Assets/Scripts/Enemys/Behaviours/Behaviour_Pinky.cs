using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Pinky : Behaviour
{
    // Start is called before the first frame update

    private int m_DistanceFromPacman = 4;
    public override void Initialize(Ghosts aGhost , PlayerController aPacman, FloorManager aFloorManager)
    {
        base.Initialize(aGhost,aPacman,aFloorManager);

        m_CalculationRefreshRate = 1;
        m_GhostSpeed = 10.5f;
        m_GoalPosition = m_Ghost.m_Pacman.m_CurrentPosition;
        m_TimerEnd = 10;
    }


    public Vector2Int GetFurthestAvaliableWallFromPacman()
    {
        Floor.FloorDirections pacmanDirection = m_Ghost.m_Pacman.m_CurrentDirection;

        List<FloorNode> nodeAheadOfPacman = new List<FloorNode>();
        
        nodeAheadOfPacman.Add(m_Ghost.m_Pacman.m_CurrentNode);

        bool loopFloornodes = true;
        
         while (true)
         {
             
             if (nodeAheadOfPacman[nodeAheadOfPacman.Count - 1] == null)
             {
                 break;
             }

             if (nodeAheadOfPacman[nodeAheadOfPacman.Count - 1].IsDirectionWalkable(pacmanDirection))
             {
                 break;
             }


             FloorNode floorNode = m_FloorManager.GetNode(m_Ghost.m_Pacman.m_CurrentPosition, pacmanDirection);
             nodeAheadOfPacman.Add(floorNode);

             if (nodeAheadOfPacman.Count > m_DistanceFromPacman)
             {
                 break;
             }

         }
        

        if (nodeAheadOfPacman.Count == 0)
        {
            return nodeAheadOfPacman[nodeAheadOfPacman.Count - 1].m_PositionInGrid;
        }
        else
        {
            return m_Pacman.m_CurrentPosition;
        }
    }

    public override void ActivateBehaviour()
    {
        m_GoalPosition = GetFurthestAvaliableWallFromPacman();
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        m_Ghost.SetGhostMaterial(m_Ghost.m_DefaultGhostMaterial);
        m_CurrentTimer = 0;
        NextMove();
    }

    public override void UpdateBehaviour()
    {
        m_CurrentTimer += Time.deltaTime;

        if (m_CurrentTimer >= m_TimerEnd)
        {
            m_Ghost.SetGhostBehaviour(Ghosts.GhostStates.Agression, true);
        }

        if (m_Paths.Count > 0)
        {
            if (m_Paths[0] != null)
            {
                DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed);
            }
        }
    }

    public override void NextMove()
    {

        if (m_Paths.Count <= 1)
        {
            ActivateBehaviour();
            return;
        }

        if (m_CurrentRefeshPosition >= m_CalculationRefreshRate)
        {
            m_GoalPosition = GetFurthestAvaliableWallFromPacman();
            m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
            m_CurrentRefeshPosition = 0;
        }
        else
        {
            m_CurrentRefeshPosition++;
        }

        m_Paths.RemoveAt(0);

    }

}