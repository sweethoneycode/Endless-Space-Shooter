using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    bool takeDamage = false;
    private Vector3 meteorScale;

    private void Awake()
    {
        float randomScale = Random.Range(0.5f, 1f);
        meteorScale = new Vector2(randomScale, randomScale);
        transform.localScale = meteorScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Missle"))
        {
            if(takeDamage)
                EventBroker.CallRestoreShields();
        }
    }

    private void Update()
    {
        if (transform.position.y < 5)
        {
            takeDamage = true;
        }
    }
}
