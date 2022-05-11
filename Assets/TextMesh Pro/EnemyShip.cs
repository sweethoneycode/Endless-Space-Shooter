using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{

    [SerializeField] EnemyController enemyController;

    [SerializeField] AudioClip impactSound;
    [SerializeField] private int points = 50;

    [SerializeField] private HealthBarBehavior HealthBarBehavior;


    [SerializeField] ChooseWeapon ChooseWeapon;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        HealthBarBehavior.SetHealth(enemyController._enemyData.health, enemyController._enemyData.maxHealth);

        currentHealth = enemyController.EnemyHealth;
        HealthBarBehavior.SetHealth(currentHealth, enemyController._enemyData.maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO determine amount of damage

        if (collision.CompareTag("Active"))
        {
            ChooseWeapon.pickWeapon(-10f, tag);

            EventBroker.CallProjectileActive();
        }

       
    }

    private void Update()
    {
        currentHealth = enemyController.EnemyHealth;
        ChangeHealth(currentHealth);
    }

    public void ChangeHealth(float enemyHealth)
    {

        HealthBarBehavior.SetHealth(enemyHealth, enemyController._enemyData.maxHealth);
    }


}
