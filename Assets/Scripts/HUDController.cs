using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

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

    [SerializeField] private Slider sheildBar;
    [SerializeField] private float CurrentShields { get; set; }
    [SerializeField] private float MaxShields { get; set; }

    [SerializeField] private float shieldChange;
    [SerializeField] private float shieldStart;
    [SerializeField] private ParticleSystem shieldbarParticles;

    [SerializeField] private AudioClip shieldAudio;

    //Load Menus

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text playerHighScoreUI;

    [SerializeField] private GameObject ResumeButton;
    [SerializeField] private GameObject EndGameButton;
    private bool restoreShields = true;

    private Scene scene;
    #endregion

    private void Awake()
    {
        SaveManager.instance.Load();
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
        EventBroker.ExtraLife += EventBroker_ExtraLife;

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

    }

    private void RestoreShields()
    {

        GetComponent<AudioSource>().PlayOneShot(shieldAudio);
        restoreShields = true;
        CurrentShields = 1;

    }

    private void PlayerHit()
    {

        if (CurrentShields > 0)
        {

            CurrentShields = CurrentShields - 0.1f;
            sheildBar.value = CurrentShields;

        }

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
        EventBroker.PlayerHit -= PlayerHit;
        EventBroker.RestoreShields -= RestoreShields;
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
        Analytics.CustomEvent("ResetLevel",
            new Dictionary<string, object>{
                    {"Level:", scene.name },
                    {"High Score:",SaveManager.instance.activeSave.highScore}
            });

        EventBroker.CallRestartGame();

        Time.timeScale = 1.0f;
        isGameOver = false;
        showShips();
        playerScore = 0;
        UpdateScore();

    }

    public void StartNewGame()
    {
        EventBroker.CallStartGame();

        gameUI.SetActive(true);

        isGameStart = true;
        showShips();

        startScreen.SetActive(false);
        scoreTXT.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();

        Analytics.CustomEvent("StartLevel",
            new Dictionary<string, object>{
                    {"Level:", scene.name },
                    {"High Score:",SaveManager.instance.activeSave.highScore}
            });
    }

    public void UpdateScore()
    {
        playerScore++;
        EnemyHasDied = false;
        scoreText.text = "Score: " + playerScore.ToString("D1");

    }

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    public void ExitGame()
    {
        Analytics.CustomEvent("QuitLevel",
            new Dictionary<string, object>{
                            {"Level:", scene.name },
                            {"High Score:",SaveManager.instance.activeSave.highScore}
            });
        SaveHighScore();

        if (isGamePaused) { Time.timeScale = 1.0f; }
        StopAllCoroutines();

        LoadingData.sceneToLoad = "MultiVerse";
        SceneManager.LoadScene("Loading");

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
            Analytics.CustomEvent("Game Over",
                new Dictionary<string, object>{
                    {"Level:", scene.name },
                    {"High Score:",SaveManager.instance.activeSave.highScore}
                });

            pauseMenu.SetActive(isGamePaused);
        }

        if (restoreShields)
        {


            if (sheildBar.value < 1)
            {
                sheildBar.value += 0.5f * Time.deltaTime;
                if (!shieldbarParticles.isPlaying)
                    shieldbarParticles.Play();

            }
            else
            {
                shieldbarParticles.Stop();
                restoreShields = false;
            }


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
                EventBroker.CallCallUpdateScore();
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
