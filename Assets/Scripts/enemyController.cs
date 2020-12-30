using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public Vector3 enemyPOS = new Vector3(0,10,0);
    private Rigidbody2D enemyRB;
    public float enemySpeed = 1f;

    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision);

        GameObject explosionInstance = Instantiate(explosionPrefab);
        explosionInstance.transform.position = transform.position;

        Destroy(explosionInstance, 1f);
        Destroy(gameObject);
        Destroy(collision.gameObject);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        enemyRB.AddForce(Vector3.down * enemySpeed, ForceMode2D.Force);

    }
}
