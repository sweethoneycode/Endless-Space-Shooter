using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    private readonly float firingCooldown = 1f;
    private float cooldownTimer;
    private Vector3 meteorScale;
    private float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Vector3 enemyPOS = meteorPrefab.transform.position;

        float randomScale = Random.Range(0.2f, 0.8f);

        meteorScale = new Vector3(randomScale, randomScale, 1);

        enemyPOS.x += Random.Range(-7, 7);
        
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = firingCooldown;

            GameObject enemyToSpawn = Instantiate(meteorPrefab, enemyPOS, meteorPrefab.transform.rotation, transform);
            enemyToSpawn.transform.localScale = meteorScale;
            
        }
    }
}
