using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour, IDamagable
{

    [SerializeField] EnemyController enemyController;
    [SerializeField] private int points = 50;
    [SerializeField] private HealthBarBehavior HealthBarBehavior;
    public float EnemyHealth { get; private set; }
    [SerializeField] ChooseWeapon ChooseWeapon;

    [Header("Sound Settings")]

    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip impactSound;





    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        LoadEnemy(enemyController._enemyData);

        HealthBarBehavior.SetHealth(enemyController._enemyData.health, enemyController._enemyData.maxHealth);

        currentHealth = EnemyHealth;
        HealthBarBehavior.SetHealth(currentHealth, enemyController._enemyData.maxHealth);


    }

    private void LoadEnemy(EnemyData data)
    {
        EnemyHealth = enemyController._enemyData.health;
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
        currentHealth = EnemyHealth;
        ChangeHealth(currentHealth);
    }

    public void ChangeHealth(float enemyHealth)
    {
        

        HealthBarBehavior.SetHealth(enemyHealth, enemyController._enemyData.maxHealth);
    }

    public void Damage(float lazorDamage, string lazorTag)
    {
        if (enemyController.takeDamage)
        {
            if (lazorTag != tag.ToString())
            {
                if (EnemyHealth >= 0)
                {
                    if(SoundManager.Instance != null)
                        SoundManager.Instance.PlayEnemySound(impactSound);

                    if (lazorDamage > 0)
                    {
                        EnemyHealth -= lazorDamage;
                    }
                    else
                    {
                        EnemyHealth--;
                    }
                    GameObject explosionInstance = Instantiate(enemyController.ExplosionPrefab);

                    explosionInstance.transform.localScale = (new Vector2(0.5f, 0.5f));
                    explosionInstance.transform.position = transform.position;
                    
                    Destroy(explosionInstance, 0.5f);
                }
                if (EnemyHealth == 0)
                {
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlayEnemySound(explosionSound);

                    enemyController.DestroyEnemy();
                    levelData.HighScore = enemyController._enemyData.points;
                    EventBroker.CallCallUpdateScore();
                }
            }
        }

    }
}
