using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour
{

    private Ghosts m_Ghost;
    private List<FloorNode> m_Paths;
    public void SetGhost(Ghosts aGhost)
    {
        m_Ghost = aGhost;
    }

    public void MoveTo()
    {

        m_Paths = CalculatePath();
        m_Ghost.transform.position = m_Paths[0].transform.position + new Vector3(0,2,0);
    }

    public void MoveTowards()
    {
        
    }

    public List<FloorNode> CalculatePath()
    {

        List<FloorNode> FinalList = new List<FloorNode>();

        FinalList = m_Ghost.m_CurrentNode.GetNeighbours(GameManager.instance.m_FloorManager.m_FloorNodes);
        

        return FinalList;
    }
}
