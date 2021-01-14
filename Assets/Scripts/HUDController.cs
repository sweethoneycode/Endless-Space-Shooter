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

    private GameContoller gameSceneController;


    private float timer;
    public int playerScore = 0;

    private int playerLives = 3;

    private bool DecreasePlayerLife = false;
    private bool EnemyHasDied = false;

    #endregion

    private void Start()
    {
        EventBroker.UpdatePlayerScore += UpdateScore;
        EventBroker.PlayerDeath += PlayerHasDied;

        //scoreText.GetComponent<TMP_Text>().text = "Timer: " + timerText;
        //scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
    }

    public void HideShip(int imageIndex)
    {
        shipImages[imageIndex].gameObject.SetActive(false);
    }

    public void UpdateScore()
    {
        playerScore++;
        EnemyHasDied = false;
        scoreText.text = playerScore.ToString("D5");
    }

    private void UpdateLives()
    {
        if (DecreasePlayerLife && playerLives > 0)
        {
            playerLives--;

            HideShip(playerLives);
            DecreasePlayerLife = false;
        }
        //   Debug.Log("Player Lives " + playerLives);

    }



    private void PlayerHasDied()
    {
        DecreasePlayerLife = true;
    }

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    //public void UpdateScore()
    //{
    //    playerScore++;
    //    EnemyHasDied = false;
    //    scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;

    //}

    // Update is called once per frame
    void Update()
    {

        UpdateLives();

        if (EnemyHasDied)
        {
            UpdateScore();
        }


        timer++;
        timerText.text = "Timer: " + timer;
    }
}
