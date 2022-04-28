using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    int laserCount;
    private bool FireOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }


    public void pickWeapon()
    {
        laserCount = 0;

        if (weapons.Length > 0)
        {
            if (levelData.HighScore > 20)
                laserCount = 1;

            if (levelData.HighScore > 100)
                laserCount = 2;

            if (levelData.HighScore > 200)
                laserCount = Random.Range(0, weapons.Length);
        }

        EventBroker.CallProjectileActive();
        GameObject laserObject = Instantiate(weapons[laserCount], transform.position, weapons[laserCount].transform.rotation, transform.parent);
    }

}

