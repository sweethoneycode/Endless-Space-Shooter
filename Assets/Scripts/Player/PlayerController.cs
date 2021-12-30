using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private Vector3 playerPos;

    [SerializeField] private GameObject missleBullet;
    private Rigidbody2D missleRB;

    private Vector2 dPadInputMovement;
    private Vector2 inputMovement;
    public float fireAction;
    private readonly float firingCooldown = 0.4f;
    private float cooldownTimer;


    private Vector2 minBounds;
    private Vector2 maxBounds;

    [SerializeField] private float paddingLeft = 0.1f;
    [SerializeField] private float paddingRight = 0.1f;
    [SerializeField] private float playerHealth = 10f;

    private bool pausedGame = true;

    // private bool isFiring = false;

    public GameObject explosionPrefab;

    private bool projectileEnabled = true;

    //Set by GameSceneController
    [SerializeField] public float speed;

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        InitBounds();
        if(speed == 0)
        {
            speed = 0.5f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y, 0);
        EventBroker.ProjectileOutOfBounds += EnableProjectile;
        EventBroker.RestoreShields += RestoreShields;
    }

    private void RestoreShields()
    {
        playerHealth = 10f;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        EventBroker.ProjectileOutOfBounds -= EnableProjectile;

    }

    public void EnableProjectile()
    {
        projectileEnabled = true;
        //availableBullet.SetActive(projectileEnabled);
    }

    // Update is called once per frame
    void Update()
    {

        playerInput.Player.Pause.performed += Pause_performed;

        //player move event
        playerMove();

        //fire event
        if (projectileEnabled)
        {
            OnFire();
        }

    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();

        EventBroker.CallPauseGame();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO determine amount of damage



        if (collision.CompareTag("Enemy"))
        {
            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.position = transform.position;
            Destroy(explosionInstance, 1f);

            if(playerHealth >= 0)
            {
                EventBroker.CallPlayerHit();
                playerHealth-- ;
            }

            if(playerHealth == 0)
            {
                EventBroker.CallPlayerDeath();
                Destroy(gameObject);
            }

          ///  Debug.Log("Player Health " + playerHealth);
        }
    }



    //Move the Player

    public void playerMove()
    {
        //using new input system
       // dPadInputMovement.x = playerInput.Player.Move.ReadValue<float>();

        inputMovement = playerInput.Player.Move.ReadValue<Vector2>();

        Vector2 currPosition = transform.position;

       if (inputMovement.x < -1 || inputMovement.x >1)
        {

            speed = 5;
        }

        currPosition.x += Mathf.Clamp(inputMovement.x, -1, 1) * Time.deltaTime * speed;

        Vector2 newPos = currPosition;
        newPos.x = Mathf.Clamp(currPosition.x, minBounds.x, maxBounds.x);

        transform.position = newPos;

    }

    public void OnFire()
    {

       // fireAction = playerInput.Player.Fire.ReadValue<float>();

        //use the float value from firing to launch missles and reduce spamming by using a bool

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = firingCooldown;

            GameObject laserObject = Instantiate(missleBullet, transform.position, missleBullet.transform.rotation, transform.parent);

            missleRB = laserObject.GetComponent<Rigidbody2D>();

            missleRB.velocity = new Vector2(0, 10f);


        }


    }
}
