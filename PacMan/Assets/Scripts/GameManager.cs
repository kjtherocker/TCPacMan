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
            m_Ghosts[i].SetPacman(m_Pacman);
            m_Ghosts[i].Initialize();

        }

        m_FloorManager.SpawnGhosts(m_Ghosts);
        
        for (int i = 0; i < m_Ghosts.Length; i++)
        {
            m_Ghosts[i].CopyFloor(m_FloorManager.m_NodeInfoArray);
        }
        
        StartPacman();
    }
    
    

    public void StartPacman()
    {
        m_Lives = 100;
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
        
        yield return new WaitForSeconds(2.0f);

        m_GhostsToActivate[0].ActivateGhostBehaviour();
        m_GhostsToActivate.RemoveAt(0);
        m_TextGetReady.gameObject.SetActive(false);
        m_Pacman.SetPacManState(PlayerController.PacmanStates.Normal);
        StartCoroutine(StartMovingGhosts());
    }

    public IEnumerator StartMovingGhosts()
    {
        if (m_GhostsToActivate.Count <= 0)
        {
            yield break;
        }

        yield return new WaitForSeconds(8.0f);
        m_GhostsToActivate[0].ActivateGhostBehaviour();
        m_GhostsToActivate.RemoveAt(0);

        StartCoroutine(StartMovingGhosts());
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


}
