using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContoller : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject timerText;

   [SerializeField] private PlayerController playerToSpawn;

    private float timer;
    public float playerScore = 0;

    private float playerLives = 3;

    private bool DecreasePlayerLife = false;
    private bool EnemyHasDied = false;

    private void Awake()
    {
        timerText.GetComponent<TMP_Text>().text = "Timer: " + timerText;
        scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
        SpawnPlayer();
    }

    private void Start()
    {
        EventBroker.PlayerDeath += PlayerHasDied;
        EventBroker.UpdatePlayerScore += EnemyDied;

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

    private void EnemyDied()
    {
        EnemyHasDied = true;
    }

    private void UpdateLives()
    {

        playerLives--;
        DecreasePlayerLife = false;

        Debug.Log("Player Lives " + playerLives);

    }

    public void UpdateScore()
    {
        playerScore++;
        EnemyHasDied = false;
        scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
    }

    private void SpawnPlayer()
    {
        Vector3 playerPOS = new Vector3(-1,-7, 1);
        PlayerController player = Instantiate(playerToSpawn );
        //player.transform.position = playerPOS;
        player.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (DecreasePlayerLife)
        {
            UpdateLives();
        }

        if (EnemyHasDied)
        {
            UpdateScore();
        }

        timer++;
        timerText.GetComponent<TMP_Text>().text = "Timer: " + timer;
    }
}
