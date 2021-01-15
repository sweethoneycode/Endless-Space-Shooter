using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContoller : MonoBehaviour
{

   [SerializeField] private PlayerController playerToSpawn;

    private float timer;
    public float playerScore = 0;

    private float playerLives = 3;

    private bool DecreasePlayerLife = false;
    private bool EnemyHasDied = false;

    private void Awake()
    {
        SpawnPlayer();
    }

    private void Start()
    {
        EventBroker.PlayerDeath += PlayerHasDied;
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

        playerLives--;
        DecreasePlayerLife = false;
        EventBroker.CallPlayerLives();

        if (playerLives > 0)
        {
            SpawnPlayer();
        }

    }

    private void SpawnPlayer()
    {
       
        Vector3 playerPOS = new Vector3(-1,-3, 1);
        PlayerController player = Instantiate(playerToSpawn );
        player.transform.position = playerPOS;
        player.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (DecreasePlayerLife && playerLives > 0)
        {

            UpdateLives();
        }
    }


}
