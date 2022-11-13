using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using UnityEngine.SceneManagement;


public class HUDController : MonoBehaviour
{
    #region Field Declarations

    [Header("UI Components")]
    [Space]
    public TMP_Text scoreText;
    public GameObject startScreen;
    [SerializeField] GameObject scoreTXT;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;
    private int numberOfShips;

    // private GameContoller gameSceneController;

    public int playerScore = 0;

    private bool isGameOver = false;
    private bool isGameStart = false;
    private bool isGamePaused = false;

    private bool EnemyHasDied = false;

    private float cooldownTimer;
    private readonly float firingCooldown = 1f;


    //Load Menus

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text playerHighScoreUI;

    [SerializeField] private GameObject ResumeButton;
    [SerializeField] private GameObject EndGameButton;


    private Scene scene;
    #endregion

    private void Awake()
    {
        SaveManager.instance.Load();

        numberOfShips = shipImages.Length;

        //Subscribe to events

        EventBroker.UpdatePlayerScore += UpdateScore;
        EventBroker.PlayerLives += HideShip;
        EventBroker.EndGame += GameOver;
        EventBroker.PauseGame += PauseGame;
        EventBroker.ExtraLife += EventBroker_ExtraLife;

        scene = SceneManager.GetActiveScene();
    }



    private void EventBroker_ExtraLife()
    {
        isGameOver = false;
        gameUI.SetActive(true);
        GetComponent<AudioSource>().Play();
        ResumeGame();
    }

    public void Start()
    {

        float playerHighScore;
        playerHighScore = SaveManager.instance.activeSave.highScore;
        playerHighScoreUI.text = "Player High Score: " + playerHighScore;

        StartNewGame();
    }



    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        EventBroker.UpdatePlayerScore -= UpdateScore;
        EventBroker.PlayerLives -= HideShip;
        EventBroker.EndGame -= GameOver;
        EventBroker.PauseGame -= PauseGame;
        EventBroker.ExtraLife -= EventBroker_ExtraLife;

    }

    private void showShips()
    {
        for (int i = 0; i < 3; i++)
        {
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

        Time.timeScale = 1.0f;
        isGameOver = false;
        showShips();
        playerScore = 0;
        UpdateScore();

    }

    public void StartNewGame()
    {


        gameUI.SetActive(true);

        isGameStart = true;
        showShips();

        startScreen.SetActive(false);
        scoreTXT.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();

        EventBroker.CallStartGame();
    }

    public void UpdateScore()
    {
        playerScore += levelData.HighScore;
        EnemyHasDied = false;
        scoreText.text = "Score: " + playerScore.ToString("D1");

    }

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    public void ExitGame()
    {

        SaveHighScore();

        if (isGamePaused) { Time.timeScale = 1.0f; }
        StopAllCoroutines();

       // LoadingData.sceneToLoad = "Start";
        SceneManager.LoadScene("Start");

    }
    public void PauseGame()
    {

        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0.0f;


        }
        else
        {

            ResumeGame();
        }



    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            pauseMenu.SetActive(isGamePaused);
        }

  

        if (isGameOver)
            gameUI.SetActive(false);

        gameOverMenu.SetActive(isGameOver);

        if (EnemyHasDied)
        {
            UpdateScore();
        }

        if (!isGameOver && isGameStart)
        {

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                cooldownTimer = firingCooldown;
             //   EventBroker.CallCallUpdateScore();
            }

        }
    }

    private void SaveHighScore()
    {
        float currentScore = SaveManager.instance.activeSave.highScore;

        if (playerScore > currentScore)
        {
            SaveManager.instance.activeSave.highScore = playerScore;
            SaveManager.instance.Save();
        }
    }
}
