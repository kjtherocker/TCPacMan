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

    private FloorManager m_FloorManager;
    private PlayerController m_Pacman;

    public void Start()
    {
        m_FloorManager = GetComponentInChildren<FloorManager>();
        m_Pacman = GetComponentInChildren<PlayerController>();
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


}
