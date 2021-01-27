using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    GameObject canvas;
    Transform goalText;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        goalText = canvas.transform.Find("ClearText");
        goalText.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            goalText.gameObject.SetActive(true);
            Invoke("ChangeScene", 3f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
