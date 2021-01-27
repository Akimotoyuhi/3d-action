using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneChange : MonoBehaviour
{
    public void OnClick()
    {
        Invoke("ChangeScene1", 1.5f);
    }
    void ChangeScene1()
    {
        SceneManager.LoadScene("Level1");
    }
}
