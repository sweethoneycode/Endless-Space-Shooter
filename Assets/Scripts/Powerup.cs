using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IConsumable
{
    bool takeDamage = false;
    private Vector3 meteorScale;

    public void Energy()
    {
        if (takeDamage)
            EventBroker.CallRestoreShields();
    }

    private void Awake()
    {
        float randomScale = Random.Range(0.5f, 1f);
        meteorScale = new Vector2(randomScale, randomScale);
        transform.localScale = meteorScale;
    }

    private void Update()
    {
        if (transform.position.y < 5)
        {
            takeDamage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (tag == "Player")
        {
            Energy();
            Destroy(gameObject, 2f);
            Destroy(collision.gameObject, 1f);
        }
    }
}
