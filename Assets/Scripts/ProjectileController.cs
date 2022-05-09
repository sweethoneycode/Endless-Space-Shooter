using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public Rigidbody2D laserRB;
    [SerializeField] public float velocityRate { private get; set; }
    [SerializeField] public float lazorDamage { private get; set; }

    [SerializeField] private AudioClip audioClip;

    // Update is called once per frame

    private void Awake()
    {
        this.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
    void Update()
    {
        if (laserRB != null)
            laserRB.velocity = new Vector2(0, velocityRate);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.Damage(lazorDamage, gameObject.tag);
            if (collision.tag != tag)
                Destroy(gameObject);
            
        }


    }
}
