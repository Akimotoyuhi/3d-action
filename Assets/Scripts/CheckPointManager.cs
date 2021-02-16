using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    GameObject manager;
    Transform text1;
    Transform text2;
    void Start()
    {
        manager = GameObject.Find("TextChangeManager");
        text1 = manager.transform.Find("TutorialText1");
        text2 = manager.transform.Find("TutorialText2");
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(true);
        }
    }
}
