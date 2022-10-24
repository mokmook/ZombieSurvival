using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    public static GameManager m_instance;

    private int score = 0;
    public bool isGameover { get; private set; }
    private void Awake()
    {
        if (instance!=this)
            Destroy(gameObject);
    }
    private void Start()
    {
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;

            UImanager.instance.UpdateScoreText(score);
        }
    }
    public void EndGame()
    {
        isGameover = true;
        UImanager.instance.SetActiveGameoverUI(true);
    }
}
