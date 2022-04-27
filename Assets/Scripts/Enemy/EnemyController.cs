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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Missle"))
        {

            if (takeDamage)
            {
                Damage();
                Destroy(collision.gameObject);
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        LoadEnemy(_enemyData);

        EnemyRB = GetComponent<Rigidbody2D>();
        EventBroker.EndGame += DestroyEnemy;
    }

    private void LoadEnemy(EnemyData data)
    {
        //foreach (Transform child in transform)
        //{
        //    if (Application.isEditor)
        //    {
        //        DestroyImmediate(child.gameObject);
        //    }
        //    else
        //    {
        //        Destroy(child.gameObject);
        //    }
        //}

        //GameObject enemyPrefab  = Instantiate(_enemyData.enemyModel);
        //enemyPrefab.transform.SetParent(transform, false);
        //enemyPrefab.transform.localPosition = Vector3.zero;
        //enemyPrefab.transform.localRotation = Quaternion.identity;
 
        EnemyHealth = _enemyData.health;
    }


    public void Damage()
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
    private void DestroyEnemy()
    {
        GameObject explosionInstance = Instantiate(ExplosionPrefab);
        explosionInstance.transform.position = transform.position;

        Destroy(explosionInstance, 1.2f);

        Destroy(transform.gameObject, 0.1f);
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

       // EnemySpeed = transform.localScale.y * _enemyData.speed;

        EnemyRB.gameObject.transform.Translate(Vector3.down * Time.deltaTime * _enemyData.speed);

    }
}
