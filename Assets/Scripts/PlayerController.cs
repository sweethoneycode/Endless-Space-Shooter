using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private Vector3 playerPos;

    public GameObject missleBullet;
    private Rigidbody2D missleRB;

    private float inputMovement;
    public float fireAction;
    private readonly float firingCooldown = 1f;
    private float cooldownTimer;

    private bool isFiring = false;

    public GameObject explosionPrefab;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y, 0);

    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //player move event
        playerMove();

        //fire event
        OnFire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO determine amount of damage

        Debug.Log("Collided " + collision.tag);

        if (collision.CompareTag("Enemy"))
        {
            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.position = transform.position;
            Destroy(explosionInstance, 1f);

            Destroy(gameObject);
        }
    }

    

    //Move the Player

    public void playerMove()
    {
        //using new input system

        inputMovement = playerInput.Player.Move.ReadValue<float>();

        Vector3 currPosition = transform.position;
        currPosition.x += inputMovement * Time.deltaTime * 5f;
        transform.position = currPosition;

    }

    public void OnFire()
    {
        fireAction = playerInput.Player.Fire.ReadValue<float>();

        //use the float value from firing to launch missles and reduce spamming by using a bool

        cooldownTimer -= Time.deltaTime;
 
        if (cooldownTimer <= 0 && fireAction == 1)
       {
            cooldownTimer = firingCooldown;
           
            GameObject laserObject = Instantiate(missleBullet, transform.position, missleBullet.transform.rotation, transform.parent);
            missleRB = laserObject.GetComponent<Rigidbody2D>();
            missleRB.velocity = new Vector2(0, 3f);
            Destroy(laserObject, 5f);

        }


    }
}
