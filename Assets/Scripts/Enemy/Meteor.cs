using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 meteorScale;
    private float lazorDamage;

    private void Awake()
    {
        float randomScale = Random.Range(0.5f, 1f);
        float randomDamage = 2;
        lazorDamage = randomDamage;

        meteorScale = new Vector2(randomScale, randomScale);
        transform.localScale = meteorScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.Damage(lazorDamage, "Enemy");
        }
    }
}
