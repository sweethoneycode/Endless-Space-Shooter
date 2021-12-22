using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void UnPause()
    {
        EventBroker.CallPauseGame();

        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync("Start");
    }
}
