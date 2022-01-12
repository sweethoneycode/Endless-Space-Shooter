using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject PowerUpPrefab;

    private float paddingLeft = 5f;
    private float paddingRight = 5f;

    private Vector2 screenBounds;

    private float cooldownTimer;
    private Vector3 meteorScale;
    private float enemySpeed;

    private bool StartAstroidWave = true;
    private bool StartShipWave = true;
    private bool StartPowerUpWave = true;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        EventBroker.EndGame += StopSpawnEnemy;
        EventBroker.RestartGame += StartSpawnEnemy;
        EventBroker.StartGame += StartSpawnEnemy;


    }

    private void StartSpawnEnemy()
    {

        StartAstroidWave = false;
        StartShipWave = false;
        StartPowerUpWave = false;
    }

    private void OnDisable()
    {
        EventBroker.EndGame -= StopSpawnEnemy;
    }

    private void StopSpawnEnemy()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartAstroidWave)
        {
            StartCoroutine(SpawnEnemies());
        }

        if (!StartShipWave)
        {
            StartCoroutine(SpawnEnemieShips());
        }

        if (!StartPowerUpWave)
        {
            StartCoroutine(SpawnPowerUps());
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        StartPowerUpWave = true;

        float secondToWait = Random.Range(10f, 30f);
        WaitForSeconds wait = new WaitForSeconds(secondToWait);

        for (int i = 0; i < 1; i++)
        {
            Vector3 enemyPOS = PowerUpPrefab.transform.position;

            float randomScale = Random.Range(0.5f, 1f);

            meteorScale = new Vector2(randomScale, randomScale);

            enemyPOS += new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y, 2);

            GameObject enemyToSpawn = Instantiate(PowerUpPrefab, enemyPOS, PowerUpPrefab.transform.rotation, transform);
            enemyToSpawn.transform.localScale = meteorScale;
            yield return wait;


        }
        StartPowerUpWave = false;
        yield return wait;
    }

    private IEnumerator SpawnEnemies()
    {
        StartAstroidWave = true;

        WaitForSeconds wait = new WaitForSeconds(2f);
        yield return wait;

        for (int i = 0; i < 50; i++)
        {
            Vector3 enemyPOS = meteorPrefab.transform.position;

            float randomScale = Random.Range(0.2f, 0.8f);

            meteorScale = new Vector2(randomScale, randomScale);

            enemyPOS += new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y, 2);

            GameObject enemyToSpawn = Instantiate(meteorPrefab, enemyPOS, meteorPrefab.transform.rotation, transform);
            enemyToSpawn.transform.localScale = meteorScale;
            yield return wait;


        }
        StartAstroidWave = false;
        yield return wait;
    }

    private IEnumerator SpawnEnemieShips()
    {
        StartShipWave = true;
        float secondToWait = Random.Range(5f, 10f);
        
        WaitForSeconds wait = new WaitForSeconds(secondToWait);
        yield return wait;

        for (int i = 0; i < 5; i++)
        {
            Vector3 enemyPOS = enemyPrefab.transform.position;

            enemyPOS += new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y, 2);

            GameObject enemyToSpawn = Instantiate(enemyPrefab, enemyPOS, enemyPrefab.transform.rotation, transform);

            float shipSpeed = Random.Range(-0.3f, -1f);

            enemyToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shipSpeed);
      

            yield return wait;
        }
        StartShipWave = false;
        yield return wait;
    }

    // TODO: Impliment IEnumerator for Enemy Spawner and features

    //private IEnumerator SpawnEnemies()
    //{
    //    WaitForSeconds wait = new WaitForSeconds(currentLevel.enemySpawnDelay);
    //    yield return wait;

    //    for (int i = 0; i < currentLevel.numberOfEnemies; i++)
    //    {
    //        Vector2 spawnPosition = ScreenBounds.RandomTopPosition();

    //        EnemyController enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //        enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
    //        enemy.shotSpeed = currentLevel.enemyShotSpeed;
    //        enemy.speed = currentLevel.enemySpeed;
    //        enemy.shotdelayTime = currentLevel.enemyShotDelay;
    //        enemy.angerdelayTime = currentLevel.enemyAngerDelay;

    //        yield return wait;
    //    }
    //}
}
