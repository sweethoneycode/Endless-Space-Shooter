using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{

    [SerializeField] public EnemyData _enemyData;

    private Rigidbody2D enemyRB;

    public float EnemyHealth { get; private set; }
    private float EnemySpeed;
    [SerializeField] private GameObject ExplosionPrefab;

    private Rigidbody2D EnemyRB { get => enemyRB; set => enemyRB = value; }

    bool takeDamage;


    // Start is called before the first frame update
    void Start()
    {
        LoadEnemy(_enemyData);

        EnemyRB = GetComponent<Rigidbody2D>();
        EventBroker.EndGame += DestroyEnemy;
    }

    private void LoadEnemy(EnemyData data)
    {
        EnemyHealth = _enemyData.health;
    }


    public void Damage(float lazorDamage, string lazorTag)
    {
        if (takeDamage)
        {
            if (lazorTag != tag.ToString())
            {
                if (EnemyHealth >= 0)
                {
                    EnemyHealth--;
                    GameObject explosionInstance = Instantiate(ExplosionPrefab);
                    explosionInstance.transform.localScale = (new Vector2(0.5f, 0.5f));
                    explosionInstance.transform.position = transform.position;
                    Destroy(explosionInstance, 0.5f);
                }
                if (EnemyHealth == 0)
                {
                    DestroyEnemy();
                    levelData.HighScore = _enemyData.points;
                    EventBroker.CallCallUpdateScore();
                }
            }
        }

    }
    private void DestroyEnemy()
    {
        GameObject explosionInstance = Instantiate(ExplosionPrefab);
        explosionInstance.transform.position = transform.position;

        Destroy(explosionInstance, 1.2f);

        Destroy(gameObject, 0.1f);
    }

    private void OnDisable()
    {
        EventBroker.EndGame -= DestroyEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 5)
        {
            takeDamage = true;
        }

        EnemyMove();
    }


    public void EnemyMove()
    {

        EnemyRB.gameObject.transform.Translate(Vector3.down * Time.deltaTime * _enemyData.speed);

    }
}
