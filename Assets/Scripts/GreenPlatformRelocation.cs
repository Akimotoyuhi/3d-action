using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatformRelocation : MonoBehaviour
{
    PlayerController m_player;
    //GameObject m_greenPlatformManager;
    GameObject m_greenPlatform;
    [SerializeField] GameObject m_greemPlatformClone = null;
    void Start()
    {
        m_player = gameObject.AddComponent<PlayerController>();
        //m_greenPlatformManager = this.gameObject;
        //m_greenPlatform = m_greenPlatformManager.transform.Find("GreenPlatform");
    }

    void Update()
    {
        m_greenPlatform = transform.Find("GreenPlatform").gameObject;
        if (m_greenPlatform == null)
        {
            Debug.Log("1");
            if (m_player.GreenPlatformRelocation())
            {
                Debug.Log("2");
                if (m_greemPlatformClone == null)
                {
                    Debug.Log("DeletePlatformにGreenPlatformのPlefabが設定されていません");
                }
                else
                {
                    Debug.Log("3");
                    Instantiate(m_greemPlatformClone, this.gameObject.transform);
                }
            }
        }
    }
}
