using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour, IDamagable
{

    [SerializeField] EnemyController enemyController;

    [SerializeField] AudioClip impactSound;
    [SerializeField] private int points = 50;

    [SerializeField] private HealthBarBehavior HealthBarBehavior;

    [SerializeField] private float firingCooldown = 1.5f;
    private float cooldownTimer;

    private int laserCount;

    [SerializeField] ChooseWeapon ChooseWeapon; 

    bool takeDamage;

    // Start is called before the first frame update
    void Start()
    {
       HealthBarBehavior.SetHealth( enemyController._enemyData.health, enemyController._enemyData.maxHealth);

    }

    // Update is called once per frame
    void Update()

    {
      // HealthBarBehavior.SetHealth(enemyController.EnemyHealth, enemyController._enemyData.maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO determine amount of damage

        if (collision.CompareTag("Active"))
        {
            ChooseWeapon.pickWeapon();
        }

            float currentHealth = enemyController.EnemyHealth;
            HealthBarBehavior.SetHealth(currentHealth, enemyController._enemyData.maxHealth);
    }



    //private void DestroyEnemy()
    //{
    //    GameObject explosionInstance = Instantiate(ExplosionPrefab);
    //    explosionInstance.transform.position = transform.position;

    //    Destroy(explosionInstance, 1.2f);

    //    levelData.HighScore = points;

    //    EventBroker.CallCallUpdateScore();

    //    Destroy(transform.gameObject, 0.2f);
    //}

    public void Damage()
    {
        levelData.HighScore = points;
        EventBroker.CallCallUpdateScore();
        HealthBarBehavior.SetHealth(enemyController.EnemyHealth, enemyController._enemyData.maxHealth);
    }
}
