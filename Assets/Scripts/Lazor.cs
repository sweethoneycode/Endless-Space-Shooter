using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazor : MonoBehaviour
{
    [SerializeField] private float firingCooldown;
    private float cooldownTimer;

    [SerializeField] private GameObject laserPrefab;
    private bool FireOn = false;

    public void Fire()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = firingCooldown;

           GameObject lazor = Instantiate(laserPrefab, transform.position, laserPrefab.transform.rotation, transform.parent);
        }
    }


    private void Update()
    {

            Fire();
    }
}
