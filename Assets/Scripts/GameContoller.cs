using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContoller : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject timerText;

    private float timer;
    public float playerScore = 0;

    private float playerLives = 3;

    private bool DecreasePlayerLife;

    private void Awake()
    {
        timerText.GetComponent<TMP_Text>().text = "Timer: " + timerText;
        scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
    }

    private void Start()
    {
        EventBroker.PlayerDeath += PlayerHasDied;
        Debug.Log("Player Lives " + playerLives);
    }

    private void OnDisable()
    {
        EventBroker.PlayerDeath -= PlayerHasDied;
    }

    private void PlayerHasDied()
    {
        DecreasePlayerLife = true;
    }

    private void UpdateLives()
    {
        if (DecreasePlayerLife)
        {
            playerLives--;
            DecreasePlayerLife = false;

            Debug.Log("Player Lives " + playerLives);
        }
    }

    public void UpdateScore(float kill)
    {
        playerScore += kill;

        scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        timerText.GetComponent<TMP_Text>().text = "Timer: " + timer;
    }
}
