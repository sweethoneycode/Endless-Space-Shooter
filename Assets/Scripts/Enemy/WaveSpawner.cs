using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private Wave currentWave;

    [SerializeField]
    private Transform[] spawnPoints;

    private float timeBtwnSpawns;
    private int i = 0;
    private bool stopSpawning = false;

    private Vector2 screenBounds;
    new Camera camera;


    private void Awake()
    {
        camera = Camera.main;
        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        currentWave = waves[i];
        timeBtwnSpawns = currentWave.TimeBeforeThisWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventBroker.EndGame += StopSpawnEnemy;
        EventBroker.RestartGame += StartSpawnEnemy;
        EventBroker.StartGame += StartSpawnEnemy;
        EventBroker.ExtraLife += StartSpawnEnemy;

    }

    private void OnDisable()
    {
        EventBroker.EndGame -= StopSpawnEnemy;
        EventBroker.RestartGame -= StartSpawnEnemy;
        EventBroker.StartGame -= StartSpawnEnemy;
        EventBroker.ExtraLife -= StartSpawnEnemy;
    }
    private void StopSpawnEnemy()
    {
        StopAllCoroutines();
    }

    private void StartSpawnEnemy()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (stopSpawning)
            return;

        if (Time.time > timeBtwnSpawns)
        {
            // SpawnWave();
            StartCoroutine(SpawnEnemies());
            IncWave();

            timeBtwnSpawns = Time.time + currentWave.TimeBeforeThisWave;
        }
    }

    private void IncWave()
    {
        if (i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
        }
        else
        {
            i = 0;
            currentWave = waves[i];
        }
    }

    private IEnumerator SpawnEnemies()
    {
        float secondToWait = Random.Range(1f, 2f);

        WaitForSeconds wait = new WaitForSeconds(secondToWait);

        for (int i = 0; i < currentWave.NumberToSpawn; i++)
        {
            int num = Random.Range(0, currentWave.EnemiesinWave.Length);
            int num2 = Random.Range(0, spawnPoints.Length);

            Vector3 enemyPOS = currentWave.EnemiesinWave[num].transform.position;
            enemyPOS += new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y, 2);


            Instantiate(currentWave.EnemiesinWave[num], enemyPOS, spawnPoints[num2].rotation, transform.parent);

            yield return wait;
        }

        yield return wait;
    }
}
