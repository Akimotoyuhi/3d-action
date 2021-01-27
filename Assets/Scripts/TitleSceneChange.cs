using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    public void OnClick()
    {
        Invoke("ChangeScene", 0.5f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
