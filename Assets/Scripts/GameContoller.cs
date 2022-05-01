using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameContoller : MonoBehaviour
{

    [SerializeField] private PlayerController playerToSpawn;

    public float playerScore = 0;
    public float checkPause;

   [SerializeField] private float playerLives;

   [SerializeField] private bool DecreasePlayerLife = false;

   [SerializeField] private AudioClip[] audioClips;

    private WaitForSeconds shipSpawnDelay = new WaitForSeconds(2);

    [Header("Player ship settings")]
    [Space]
    [Range(3, 8)]
    [SerializeField] private float playerSpeed = 7;

    private bool pressedPause;

    public PlayerInput playerInput;

    private void Start()
    {
        EventBroker.PlayerDeath += PlayerHasDied;
        EventBroker.RestartGame += RestartGame;
        EventBroker.StartGame += NewGame;
        EventBroker.ExtraLife += EventBroker_ExtraLife;

        playerInput = new PlayerInput();

        NewGame();
    }

    private void EventBroker_ExtraLife()
    {
        GetComponent<AudioSource>().Play();
        playerLives += 1;
        StartCoroutine(SpawnShip(true));
    }

    private void NewGame()
    {
        StartCoroutine(SpawnShip(false));
    }

    private void OnDisable()
    {
        EventBroker.PlayerDeath -= PlayerHasDied;
        EventBroker.RestartGame -= RestartGame;
        EventBroker.StartGame -= NewGame;
        EventBroker.ExtraLife -= EventBroker_ExtraLife;
    }

    private void PlayerHasDied()
    {

        DecreasePlayerLife = true;
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        EventBroker.CallRestoreShields();

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateLives();

    }


}
