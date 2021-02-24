using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public TextMeshProUGUI m_TextScore;
    public TextMeshProUGUI m_TextLives;
    public GameObject m_TextGetReady;
    
    private int m_Lives;
    private int m_Score;

    public FloorManager m_FloorManager;
    public PlayerController m_Pacman;
    private Ghosts[] m_Ghosts;
    private List<Ghosts> m_GhostsToActivate;
    
    private float m_GhostMoveDelay = 2;
    
    public void Start()
    {
        m_FloorManager = GetComponentInChildren<FloorManager>();
        m_Pacman = GetComponentInChildren<PlayerController>();
        m_Ghosts = GetComponentsInChildren<Ghosts>();

        m_GhostsToActivate = new List<Ghosts>();
        for (int i = 0; i < m_Ghosts.Length; i++)
        {
            m_GhostsToActivate.Add(m_Ghosts[i]);
        }

        m_FloorManager.Start();
        m_FloorManager.SpawnNodeInfo();
        //Initializing the ghosts 
        for (int i = 0; i < m_Ghosts.Length; i++)
        {
            m_Ghosts[i].Initialize(m_Pacman , m_FloorManager);
        }

        m_FloorManager.SpawnGhosts(m_Ghosts);
        
        for (int i = 0; i < m_Ghosts.Length; i++)
        {
            float timeMultiplyer = i + 1;
            m_Ghosts[i].CopyFloor(m_FloorManager.m_NodeInfoArray);
            m_Ghosts[i].GetGhostBehaviour(Ghosts.GhostStates.Standby).SetTimer(m_GhostMoveDelay * timeMultiplyer);
        }
        
        StartPacman();
    }
    
    

    public void StartPacman()
    {
        m_Lives = 3;
        m_Score = 0;

        m_TextScore.text = m_Score.ToString();
        m_TextLives.text = m_Lives.ToString();

        m_FloorManager.GenerateGimmicks();
        m_FloorManager.m_PacMan = m_Pacman;
        
        m_Pacman.SetPacManState(PlayerController.PacmanStates.Start);
        m_Pacman.ReturnToSpawn();
        
        StartCoroutine(StartDelay());
        
        
        m_TextGetReady.gameObject.SetActive(true);

    }
    
    public IEnumerator StartDelay()
    {
        m_Pacman.SetPacManState(PlayerController.PacmanStates.Start);
        m_TextGetReady.gameObject.SetActive(true);
        
        for (int i = 0; i < m_Ghosts.Length; i++)
        {
            m_Ghosts[i].SetGhostBehaviour(Ghosts.GhostStates.Standby,true);
        }
        
        yield return new WaitForSeconds(2.0f);


        m_TextGetReady.gameObject.SetActive(false);
        m_Pacman.SetPacManState(PlayerController.PacmanStates.Normal);

    }


    public void ChangeScore(int aScore)
    {
        m_Score += aScore;
        m_TextScore.text = m_Score.ToString();
    }



    public void ChangeLives(int aLives)
    {
        if (m_Lives + aLives <= 0)
        {
            StartPacman();
            return;
        }

        m_Lives += aLives;
        m_TextLives.text = m_Lives.ToString();
        m_Pacman.ReturnToSpawn();
        StartCoroutine(StartDelay());
    }

    public Ghosts GetGhost(Ghosts.GhostTypes aGhostTypes)
    {
        for (int i = 0; i < m_Ghosts.Length - 1; i++)
        {
            if (m_Ghosts[i].m_GhostType == aGhostTypes)
            {
                return m_Ghosts[i];
            }
        }

        return null;
    }

    public void MakeGhostsEatable()
    {
        for (int i = 0; i < m_Ghosts.Length; i++)
        {
            m_Ghosts[i].SetGhostBehaviour(Ghosts.GhostStates.Eatable,true);
        }
    }


}
