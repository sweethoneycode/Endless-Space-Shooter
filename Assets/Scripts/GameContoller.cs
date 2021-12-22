using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameContoller : MonoBehaviour
{

    [SerializeField] private PlayerController playerToSpawn;

    private float timer;
    public float playerScore = 0;
    public float checkPause;

   [SerializeField] private float playerLives = 3;

   [SerializeField] private bool DecreasePlayerLife = false;

    private WaitForSeconds shipSpawnDelay = new WaitForSeconds(2);

    [Header("Player ship settings")]
    [Space]
    [Range(3, 8)]
    public float playerSpeed = 7;

    private bool pressedPause;

    public PlayerInput playerInput;


    private void Start()
    {
        EventBroker.PlayerDeath += PlayerHasDied;
        EventBroker.RestartGame += RestartGame;
        EventBroker.StartGame += NewGame;

        playerInput = new PlayerInput();
    }

    private void NewGame()
    {
        playerLives = 3;
        StartCoroutine(SpawnShip(false));
    }

    private void OnDisable()
    {
        EventBroker.PlayerDeath -= PlayerHasDied;
        EventBroker.RestartGame -= RestartGame;
        EventBroker.StartGame -= NewGame;

    }

    private void PlayerHasDied()
    {

        DecreasePlayerLife = true;
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("Classic");
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
                StartCoroutine(SpawnShip(true));
            }

            if (playerLives == 0)
            {
                StopAllCoroutines();
                EventBroker.CallEndGame();
            }
        }



    }

    private IEnumerator SpawnShip(bool delayed)
    {
        if (delayed)
            yield return shipSpawnDelay;

        PlayerController ship = Instantiate(playerToSpawn, new Vector2(0, -3f), Quaternion.identity);
       ship.speed = playerSpeed;
        // ship.shieldDuration = shieldDuration;
        EventBroker.CallRestoreShields();

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateLives();

    }


}
