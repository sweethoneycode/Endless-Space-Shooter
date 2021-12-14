using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{

    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float EnemyHealth;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip impactSound;

    private Rigidbody2D laserRB;
    private readonly float firingCooldown = 0.5f;
    private float cooldownTimer;

    //Set by GameSceneController
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 5) { 
            OnFire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO determine amount of damage

        if (collision.CompareTag("Missle"))
        {

            if(EnemyHealth >= 0)
            {
                EnemyHealth--;
                GetComponent<AudioSource>().PlayOneShot(impactSound);
                Destroy(collision.gameObject);
                GameObject explosionInstance = Instantiate(ExplosionPrefab);
                explosionInstance.transform.localScale = (new Vector2(0.5f, 0.5f));
                explosionInstance.transform.position = transform.position;
                Destroy(explosionInstance, 0.5f);
            }

            if (EnemyHealth == 0)
            {

                EventBroker.CallCallUpdateScore();

                DestroyEnemy();


                Destroy(collision.gameObject);
            }

        }
    }

    private void DestroyEnemy()
    {
        GameObject explosionInstance = Instantiate(ExplosionPrefab);
        explosionInstance.transform.position = transform.position;

        GetComponent<AudioSource>().PlayOneShot(explosionSound);

        Destroy(explosionInstance, 1.2f);

        Destroy(transform.gameObject, 0.2f);
    }

    public void OnFire()
    {

        //use the float value from firing to launch missles and reduce spamming by using a bool

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = firingCooldown;

            GameObject laserObject = Instantiate(laserPrefab, transform.position, laserPrefab.transform.rotation, transform.parent);

            laserObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10f);


        }


    }
}
