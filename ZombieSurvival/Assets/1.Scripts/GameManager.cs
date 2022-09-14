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
    public bool isGameover;
    private void Awake()
    {
        if (instance!=this)
            Destroy(gameObject);
    }
}
