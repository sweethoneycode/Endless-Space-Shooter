using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    #region Field Declarations

    [Header("UI Components")]
    [Space]
    public TMP_Text scoreText;
    public TMP_Text timerText;

    //   public StatusText statusText;
    public Button restartButton;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;
    private int numberOfShips;

    private GameContoller gameSceneController;


    private float timer;
    public int playerScore = 0;


    private bool DecreasePlayerLife = false;
    private bool EnemyHasDied = false;

    #endregion

    private void Start()
    {
        numberOfShips = shipImages.Length;
        EventBroker.UpdatePlayerScore += UpdateScore;
        EventBroker.PlayerDeath += PlayerHasDied;
        EventBroker.PlayerLives += HideShip;
    }

    public void HideShip()
    {
        if (numberOfShips > 0)
        {
            numberOfShips--;
            shipImages[numberOfShips].gameObject.SetActive(false);
        }
    }

    public void UpdateScore()
    {
        playerScore++;
        EnemyHasDied = false;
        scoreText.text = playerScore.ToString("D5");
    }


    private void PlayerHasDied()
    {
        DecreasePlayerLife = true;
    }

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (EnemyHasDied)
        {
            UpdateScore();
        }


        timer++;
        timerText.text = "Timer: " + timer;
    }
}
