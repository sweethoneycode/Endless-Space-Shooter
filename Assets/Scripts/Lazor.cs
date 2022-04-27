using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazor : MonoBehaviour
{
    [SerializeField] private float firingCooldown = 1.5f;
    private float cooldownTimer;
    [SerializeField] private GameObject laserPrefab;
    private bool FireOn = false;

    public void Fire()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = firingCooldown;

            Instantiate(laserPrefab, transform.position, laserPrefab.transform.rotation, transform.parent);

        }
    }

    void Start()
    {

        EventBroker.ProtectileActive += EventBroker_ProtectileActive;
    }

    private void OnDestroy()
    {
        EventBroker.ProtectileActive -= EventBroker_ProtectileActive;
    }

    private void OnDisable()
    {
        EventBroker.ProtectileActive -= EventBroker_ProtectileActive;
    }

    private void EventBroker_ProtectileActive()
    {
        FireOn = true;
    }

    private void Update()
    {
        if (FireOn)
        {
            Fire();
        }
    }
}
