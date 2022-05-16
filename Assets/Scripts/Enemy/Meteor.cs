using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour, IDamagable
{
    private Vector3 meteorScale;
    private float lazorDamage;
    [SerializeField] EnemyController enemyController;

    public void Damage(float lazorDamage, string lazorTag)
    {
        GameObject explosionInstance = Instantiate(enemyController.ExplosionPrefab);

        explosionInstance.transform.localScale = (new Vector2(0.5f, 0.5f));
        explosionInstance.transform.position = transform.position;

        Destroy(explosionInstance, 0.5f);
    }

    private void Awake()
    {
        lazorDamage = enemyController._enemyData.damage;

        float randomScale = Random.Range(0.5f, 1f);

        meteorScale = new Vector2(randomScale, randomScale);
        transform.localScale = meteorScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            Debug.Log("Damage " + lazorDamage);
            damagable.Damage(lazorDamage, "Meteor");
        }
    }


}
