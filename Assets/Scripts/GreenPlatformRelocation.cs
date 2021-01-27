using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatformRelocation : MonoBehaviour
{
    PlayerController m_player = new PlayerController();
    GameObject m_greenPlatformManager;
    Transform m_greenPlatform;
    [SerializeField] GameObject m_greemPlatformClone = null;
    void Start()
    {
        m_greenPlatformManager = this.gameObject;
        m_greenPlatform = m_greenPlatformManager.transform.Find("GreenPlatform");
    }

    void Update()
    {
        if (m_greenPlatform == null)
        {
            if (m_player.GreenPlatformRelocation())
            {
                if (m_greemPlatformClone == null)
                {
                    Debug.Log("DeletePlatformにGreenPlatformのPlefabが設定されていません");
                }
                else
                {
                    Instantiate(m_greemPlatformClone, this.gameObject.transform);
                }
            }
        }
    }
}
