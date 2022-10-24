using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UImanager>();
            }
            return m_instance;
        }
    }
    public static UImanager m_instance;

    [SerializeField] Text ammoText;
    [SerializeField] Text scoreText;
    [SerializeField] Text waveText;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] Button reStartBtn;
    private void Start()
    {
        reStartBtn.onClick.AddListener(() => GameRestart());
    }
    public void UpdateAmmoText(int magAmmo,int ammoRemain)
    {
        ammoText.text = magAmmo + "/" + ammoRemain;
    }
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;  
    }
    public void UpdateWaveText(int waves,int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count; 
    }
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }
    private void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
