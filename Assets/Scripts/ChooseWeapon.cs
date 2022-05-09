using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    private float cooldownTimer;
    private float delayFire;
    private float newVelocityRate;
    private string newWeaponTag;
    public float weaponDamage;

    private bool FireOn = false;
    // Start is called before the first frame update


    public void pickWeapon(float velocityRate, string weaponTag)
    {
        if (weaponData != null)
        {
            FireOn = true;
            delayFire = weaponData.fireDelay;
            newVelocityRate = velocityRate;
            newWeaponTag = weaponTag;
        }
    }

    public IEnumerator Fire()
    {

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = weaponData.coolDown;
            GameObject lazor = Instantiate(weaponData.weapon, transform.position, weaponData.weapon.transform.rotation, transform.parent);
            lazor.GetComponent<ProjectileController>().velocityRate = newVelocityRate;
            lazor.tag = newWeaponTag;
            lazor.GetComponent<ProjectileController>().lazorDamage = weaponDamage;

            yield return new WaitForSeconds(delayFire);
        }
    }



    private void Update()
    {
        if (FireOn)
            StartCoroutine(Fire());
    }
}

