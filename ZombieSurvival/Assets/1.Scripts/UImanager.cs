using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UpdateAmmoText(int magAmmo,int ammoRemain)
    {

    }
}
