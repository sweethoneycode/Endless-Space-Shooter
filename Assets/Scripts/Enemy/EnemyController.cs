using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 enemyPOS = new Vector3(0, 10, 0);
    private Rigidbody2D enemyRB;

    [SerializeField] private float EnemyHealth = 1f;
    [SerializeField] private float EnemySpeed = 1f;
    [SerializeField] private GameObject ExplosionPrefab;

    // public GameContoller gameContoller;

    private GameObject explosionPrefab;

    private Vector3 EnemyPOS { get => enemyPOS; set => enemyPOS = value; }
    private Rigidbody2D EnemyRB { get => enemyRB; set => enemyRB = value; }

    bool takeDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Missle") && takeDamage)
        {

            EventBroker.CallCallUpdateScore();

            DestroyEnemy();


            Destroy(collision.gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        EventBroker.EndGame += DestroyEnemy;
    }

    private void DestroyEnemy()
    {
       GameObject explosionInstance = Instantiate(ExplosionPrefab);
        explosionInstance.transform.position = transform.position;

        Destroy(explosionInstance, 1.2f);

        Destroy(transform.gameObject,0.1f);
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

       EnemySpeed = transform.localScale.y * 5;

        EnemyRB.gameObject.transform.Translate(Vector3.down * Time.deltaTime * EnemySpeed);
    

        if(transform.position.y <= -5)
        {
            Destroy(gameObject);
        }


    }
}
