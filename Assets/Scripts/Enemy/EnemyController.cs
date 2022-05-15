using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] public EnemyData _enemyData;

    private Rigidbody2D enemyRB;


    private float EnemySpeed;
    public GameObject ExplosionPrefab;

    private Rigidbody2D EnemyRB { get => enemyRB; set => enemyRB = value; }

    public bool takeDamage;


    // Start is called before the first frame update
    void Start()
    {


        EnemyRB = GetComponent<Rigidbody2D>();
        EventBroker.EndGame += DestroyEnemy;
    }

    public void DestroyEnemy()
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
