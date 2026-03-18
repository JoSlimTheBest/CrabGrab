using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChanger : MonoBehaviour
{
    public void FirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SceneQuit()
    {
        Application.Quit();
    }
}
