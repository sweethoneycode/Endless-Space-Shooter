using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 enemyPOS = new Vector3(0,10,0);
    private Rigidbody2D enemyRB;

    public float enemySpeed = 1f;

   // public GameContoller gameContoller;

    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Missle"))
        {

            EventBroker.CallCallUpdateScore();

            DestroyEnemy();


            Destroy(collision.gameObject);
        }
        
    }

    private void Awake()
    {
      //  gameContoller = FindObjectOfType<GameContoller>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        EventBroker.EndGame += DestroyEnemy;
    }

    private void DestroyEnemy()
    {
       GameObject explosionInstance = Instantiate(explosionPrefab);
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
        EnemyMove();
    }

    public void EnemyMove()
    {

       enemySpeed = transform.localScale.y * 5;

        enemyRB.gameObject.transform.Translate(Vector3.down * Time.deltaTime * enemySpeed);
       //enemyRB.AddForce(Vector3.down * enemySpeed, ForceMode2D.Force);
    
        if(transform.position.y <= -6)
        {
            Destroy(gameObject);
        }


    }
}
