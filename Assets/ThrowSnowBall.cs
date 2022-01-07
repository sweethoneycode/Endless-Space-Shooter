using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowBall : MonoBehaviour
{
    [SerializeField] private readonly float firingCooldown = 1.5f;
    private float cooldownTimer;
    [SerializeField] private Animator anim;
    private bool fireSnowBall;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fireSnowBall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 5)
        {
            SnowBall();
        }
}

    private void SnowBall()
    {

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)

        {
            cooldownTimer = firingCooldown;
            anim.SetBool("fire", true);

        }

        anim.SetBool("fire", false);
    }
}
