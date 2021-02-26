using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    private float cooldownTimer;
    private Vector3 meteorScale;
    private float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        EventBroker.EndGame += StopSpawnEnemy;
        EventBroker.RestartGame += StartSpawnEnemy;
        EventBroker.StartGame += StartSpawnEnemy;
    }

    private void StartSpawnEnemy()
    {
        StartCoroutine(SpawnEnemies());
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

    }

    private IEnumerator SpawnEnemies()
    {

        WaitForSeconds wait = new WaitForSeconds(3f);
        yield return wait;

        for (int i = 0; i < 1000; i++)
        {
            Vector3 enemyPOS = meteorPrefab.transform.position;

            float randomScale = Random.Range(0.2f, 0.8f);

            meteorScale = new Vector3(randomScale, randomScale, 1);

            enemyPOS.x += Random.Range(-7, 7);

            GameObject enemyToSpawn = Instantiate(meteorPrefab, enemyPOS, meteorPrefab.transform.rotation, transform);
            enemyToSpawn.transform.localScale = meteorScale;
            yield return wait;
        }

        
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
