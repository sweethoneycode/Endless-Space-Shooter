using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D laserRB;
    [SerializeField] AudioClip impactSound;

    // Update is called once per frame
    void Update()
    {
        if (laserRB != null)
            laserRB.velocity = new Vector2(0, -10f);

    }

}
