using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour
{

    protected Ghosts m_Ghost;
    protected List<FloorNode> m_Paths;
    protected int m_CalculationRefreshRate = 2;
    protected int m_CurrentRefeshPosition;

    protected Vector2Int m_GoalPosition;
    
    protected float m_GhostSpeed = 7.5f;

    protected float m_TimerEnd;
    protected float m_CurrentTimer;

    protected Material m_GhostMaterial;
    
    public virtual void Initialize()
    {
        
    }

    public void SetTimer(float aTimer)
    {
        m_TimerEnd = aTimer;
    }

    public void SetGhost(Ghosts aGhost)
    {
        m_Ghost = aGhost;
    }

    public virtual void ActivateBehaviour()
    {
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        NextMove();
    }
     
    public virtual void UpdateBehaviour()
    {
        if (m_Paths[0] != null)
        {
            DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed);
        }
    }

    public virtual void NextMove()
    {
        if (m_Paths.Count <= 0)
        {
            ActivateBehaviour();
            return;
        }

        if (m_CurrentRefeshPosition >= m_CalculationRefreshRate)
        {
            m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
            m_CurrentRefeshPosition = 0;
        }
        else
        {
            m_CurrentRefeshPosition++;
        }

        m_Ghost.SetCurrentFloorNode(m_Paths[0]);
        m_Paths.RemoveAt(0);
        
        
    }

    public virtual void PacmanContact()
    {
        GameManager.instance.ChangeLives(-1);
    }


    public  void DirectMovement(Transform aObject, FloorNode  aTargetNode, float aTimeUntilDone)
    {
        Vector3 NewNodePosition = new Vector3(aTargetNode.transform.position.x,aTargetNode.transform.position.y + 2,
            aTargetNode.transform.position.z);

        float timeTaken = 0.0f;
 
        

        if (Vector3.Distance(aObject.transform.position, NewNodePosition) < 0.25f)
        {
            m_Ghost.m_CurrentNode = aTargetNode;
            NextMove();    
            return;
        }
        
        aObject.position = Vector3.MoveTowards(aObject.position, NewNodePosition, aTimeUntilDone * Time.deltaTime);

        
    }

}
