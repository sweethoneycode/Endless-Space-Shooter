using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{

    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float EnemyHealth;
    [SerializeField] private float MaxHealth;
    [SerializeField] AudioClip impactSound;

    [SerializeField] private HealthBarBehavior HealthBarBehavior;

    private Rigidbody2D laserRB;
    [SerializeField] private readonly float firingCooldown = 1.5f;
    private float cooldownTimer;

    //Set by GameSceneController
    [SerializeField] private float speed;

    bool takeDamage;

    // Start is called before the first frame update
    void Start()
    {
        HealthBarBehavior.SetHealth(EnemyHealth, MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 5) { 
            OnFire();
        }

        if (transform.position.y < 5)
        {
            takeDamage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO determine amount of damage

        if (collision.CompareTag("Missle"))
        {
            if (takeDamage)
            {
                if (EnemyHealth >= 0)
                {
                    EnemyHealth--;

                    HealthBarBehavior.SetHealth(EnemyHealth, MaxHealth);

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
    }

    private void DestroyEnemy()
    {
        GameObject explosionInstance = Instantiate(ExplosionPrefab);
        explosionInstance.transform.position = transform.position;

        Destroy(explosionInstance, 1.2f);

        EventBroker.CallCallUpdateScore();
//        EventBroker.CallRestoreShields();

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
