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
    
    protected float m_GhostSpeed = 2.5f;
    public virtual void Initialize()
    {
        
    }

    public void SetGhost(Ghosts aGhost)
    {
        m_Ghost = aGhost;
    }

    public virtual void StartMoving()
    {
        m_Paths = m_Ghost.CalculatePath(m_GoalPosition);
        NextMove();
    }
     
    public virtual void UpdateBehaviour()
    {

    }

    public virtual void NextMove()
    {
        if (m_Paths.Count <= 0)
        {
            StartMoving();
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
        
        Debug.Log(m_Ghost.transform.position  + "  and " + m_Paths[0].gameObject.transform.position);
        
        m_Ghost.ProcessBehaviour(DirectMovement(m_Ghost.gameObject.transform, m_Paths[0], m_GhostSpeed));

        
    }

    
    public  IEnumerator DirectMovement(Transform aObject, FloorNode  aTargetNode, float aTimeUntilDone)
    {
        Vector3 NewNodePosition = new Vector3(aTargetNode.transform.position.x,aTargetNode.transform.position.y + 2,
            aTargetNode.transform.position.z);

        float timeTaken = 0.0f;

        while (aTimeUntilDone - timeTaken > 0)
        {
            if (Vector3.Distance(aObject.transform.position, NewNodePosition) < 0.00001f)
            {
                timeTaken = aTimeUntilDone;
            }

            timeTaken += Time.deltaTime;
            aObject.position = Vector3.MoveTowards(aObject.position, NewNodePosition, timeTaken /aTimeUntilDone );
            yield return null;
        }

        aObject.position = NewNodePosition;
        m_Ghost.SetCurrentFloorNode(aTargetNode);
        NextMove();
        yield return 0;
    }

}
