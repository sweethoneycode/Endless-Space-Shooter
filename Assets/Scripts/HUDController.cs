using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    #region Field Declarations

    [Header("UI Components")]
    [Space]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject endScreen;
    public GameObject startScreen;

    //   public StatusText statusText;
  //  public Button restartButton;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;
    private int numberOfShips;

    private GameContoller gameSceneController;


    private float timer;
    public int playerScore = 0;

    private bool isGameOver = false;
    private bool isGameStart = false;

    private bool EnemyHasDied = false;

    #endregion

    private void Start()
    {
        numberOfShips = shipImages.Length;
        
        //Subscribe to events

        EventBroker.UpdatePlayerScore += UpdateScore;
        EventBroker.PlayerLives += HideShip;
        EventBroker.EndGame += GameOver;

    }

    private void GameOver()
    {
        isGameOver = true;
    }

    private void OnDisable()
    {
        EventBroker.EndGame -= GameOver;
    }

    private void showShips()
    {
        for (int i = 0; i < 3; i++){
            shipImages[i].gameObject.SetActive(true);
        }
    }
    public void HideShip()
    {
        if (numberOfShips > 0)
        {
            numberOfShips--;
            shipImages[numberOfShips].gameObject.SetActive(false);
        }
    }

    public void ResetGame()
    {
        EventBroker.CallRestartGame();

        isGameOver = false;
        timer = 0;
        showShips();
        playerScore = 0;
        UpdateScore();

    }

    public void StartNewGame()
    {
        EventBroker.CallStartGame();
        isGameStart = true;
        timer = 0;
        showShips();
    }

    public void UpdateScore()
    {
        playerScore++;
        EnemyHasDied = false;
        scoreText.text = playerScore.ToString("D5");
    }

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            startScreen.SetActive(false);
        }

        if (isGameOver)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
        }

        if (EnemyHasDied)
        {
            UpdateScore();
        }

        if (!isGameOver && isGameStart)
        {
           
            timer++;
            timerText.text = "Timer: " + timer;
        }
    }
}
