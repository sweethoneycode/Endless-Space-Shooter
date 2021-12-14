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
    public GameObject pauseScreen;

    //   public StatusText statusText;
    //  public Button restartButton;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;
    private int numberOfShips;

    private GameContoller gameSceneController;


    private float timer = 0.0f;
    private float waitTime = 2.0f;
    private float visualTime = 0.0f;

    public int playerScore = 0;

    private bool isGameOver = false;
    private bool isGameStart = false;
    private bool isGamePaused = false;

    private bool EnemyHasDied = false;

    private float cooldownTimer;
    private readonly float firingCooldown = 1f;

    [SerializeField] private Slider sheildBar;
    [SerializeField] private float CurrentShields { get; set; }
    [SerializeField] private float MaxShields { get; set; }

    [SerializeField] private float shieldChange;
    [SerializeField] private float shieldStart;

    #endregion

    private void Start()
    {

        MaxShields = 1f;
        CurrentShields = MaxShields;

        shieldStart = 8f;
        shieldChange = 4f;

        numberOfShips = shipImages.Length;
        
        //Subscribe to events

        EventBroker.UpdatePlayerScore += UpdateScore;
        EventBroker.PlayerLives += HideShip;
        EventBroker.EndGame += GameOver;
        EventBroker.PauseGame += PauseGame;
        EventBroker.PlayerHit += PlayerHit;
        EventBroker.RestoreShields += RestoreShields;
    }

    private void RestoreShields()
    {
        CurrentShields = 1f;
        sheildBar.value = CurrentShields;
    }

    private void PlayerHit()
    {
        Debug.Log("Player Hit");

        if (CurrentShields > 0)
        {

            CurrentShields = CurrentShields - 0.1f;
            sheildBar.value = CurrentShields;
     
        }

   //     Debug.Log("Shield " + CurrentShields);
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
        scoreText.text = "Score: " + "\n" + playerScore.ToString("D1");

    }

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {

        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0.0f;
        } else
        {
            ResumeGame();
        }

      //  Debug.Log("Is Game Paused " + isGamePaused);

    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {


        if (isGameStart)
        {
            startScreen.SetActive(false);
        }


       pauseScreen.SetActive(isGamePaused);



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
            

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0 )
            {
                cooldownTimer = firingCooldown;
                timer++;
                timerText.text = "Distance: " + "\n" + timer;
            }
 
        }
    }


}
