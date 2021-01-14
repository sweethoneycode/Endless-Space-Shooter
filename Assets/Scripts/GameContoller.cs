using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContoller : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;

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
        EventBroker.UpdatePlayerScore += EnemyDied;
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

        SpawnPlayer();

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
