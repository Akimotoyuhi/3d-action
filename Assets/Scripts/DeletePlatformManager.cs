using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlatformManager : MonoBehaviour
{
    bool m_flag;
    Color alpha = new Color(0, 0, 0, 0.01f);
    MeshRenderer m_mesh;

    void Start()
    {
        m_mesh = GetComponent<MeshRenderer>();
        m_flag = false;
    }


    void Update()
    {
        if (m_flag)
        {
            m_mesh.material.color -= alpha;
            
            if (m_mesh.material.color.a < 0.1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!m_flag)
            {
                m_flag = true;
            }
        }
    }
}
