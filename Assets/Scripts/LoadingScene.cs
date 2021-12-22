using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private TMP_Text percentLoaded;
    [SerializeField] private CanvasGroup canvasGroup;

    public GameObject loadingScreen;

    AsyncOperation loadingOperation;

    void Start()
    {
        StartCoroutine(StartLoad());

    }

    void Update()
    {

    }

    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
        while (!operation.isDone)
        {
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
            yield return null;

        }

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
