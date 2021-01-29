using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameContoller : MonoBehaviour
{

    [SerializeField] private PlayerController playerToSpawn;

    private float timer;
    public float playerScore = 0;

    private float playerLives = 3;

    private bool DecreasePlayerLife = false;
    private bool EnemyHasDied = false;
    private bool isNewGame = false;

    private void Awake()
    {
        SpawnPlayer();
    }

    private void Start()
    {
        EventBroker.PlayerDeath += PlayerHasDied;
        EventBroker.RestartGame += NewGame;
    }

    private void NewGame()
    {
        playerLives = 3;
        SpawnPlayer();
    }

    private void OnDisable()
    {
        EventBroker.PlayerDeath -= PlayerHasDied;
        EventBroker.RestartGame -= NewGame;
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

            EventBroker.CallPlayerLives();
            
            if (playerLives > 0)
            {
                SpawnPlayer();
            }

            if (playerLives == 0)
            {
                EventBroker.CallEndGame();
            }
        }



    }

    private void SpawnPlayer()
    {

        Vector3 playerPOS = new Vector3(-1, -3, 1);
        PlayerController player = Instantiate(playerToSpawn);
        player.transform.position = playerPOS;
        player.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        UpdateLives();

    }


}
