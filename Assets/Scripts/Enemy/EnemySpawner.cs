using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private float paddingLeft = 5f;
    private float paddingRight = 5f;

    private Vector2 screenBounds;

    private float cooldownTimer;
    private Vector3 meteorScale;
    private float enemySpeed;

    private bool StartAstroidWave = true;
    private bool StartShipWave = true;
    new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));

        EventBroker.EndGame += StopSpawnEnemy;
        EventBroker.RestartGame += StartSpawnEnemy;
        EventBroker.StartGame += StartSpawnEnemy;
        EventBroker.ExtraLife += StartSpawnEnemy;

    }

    private void StartSpawnEnemy()
    {

        StartAstroidWave = false;
        StartShipWave = false;
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

    }

    private IEnumerator SpawnEnemies()
    {
        StartAstroidWave = true;

        float secondToWait = Random.Range(1f, 2f);

        WaitForSeconds wait = new WaitForSeconds(secondToWait);

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
        float secondToWait = Random.Range(3f, 5f);

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
}
