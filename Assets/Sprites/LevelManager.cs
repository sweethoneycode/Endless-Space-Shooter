using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string targetScene;

    public void Awake()
    {
     //   DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string targetScene)
    {
        LoadingData.sceneToLoad = targetScene;
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
