using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    private float playerScore;
    [SerializeField] private TMP_Text currentPlayerScore;
    [SerializeField] private AudioClip AudioClip;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerScore = SaveManager.instance.activeSave.highScore;
        currentPlayerScore.text = "Player High Score: " + playerScore.ToString();

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayStageMusic(AudioClip);
    }

}
