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
    private Vector2 inputMovement;
    public float fireAction;
    private readonly float firingCooldown = 0.4f;
    private float cooldownTimer;


    private Vector2 minBounds;
    private Vector2 maxBounds;

    private float paddingLeft = 0.1f;
    private float paddingRight = 0.1f;
    [SerializeField] private float playerHealth = 10f;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip playHit;

    public GameObject explosionPrefab;

    [SerializeField] private bool projectileEnabled = true;

    //Set by GameSceneController
    [SerializeField] public float speed;

    float waitTimer;

    int playerPoints;
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
    }

    // Update is called once per frame
    void Update()
    {

        playerInput.Player.Pause.performed += Pause_performed;

        //player move event


        //fire event
        if (projectileEnabled)
        {
            playerMove();
            OnFire();
        }

    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {

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

            if (playerHealth >= 0)
            {
                playerHealth--;
                EventBroker.CallPlayerHit();
                animator.Play("shipHit");

            }

            if (playerHealth == 0)
            {

                StartCoroutine(PlayerDeath());
            }

        }
    }

    private IEnumerator PlayerDeath() 
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        EventBroker.CallPlayerDeath();
        Destroy(gameObject);
        yield return wait;
        
    }

    //Move the Player

    public void playerMove()
    {
        inputMovement = playerInput.Player.Move.ReadValue<Vector2>();

        Vector2 currPosition = transform.position;

        if (inputMovement.x < -1 || inputMovement.x > 1)
        {
            if (Application.platform == RuntimePlatform.WSAPlayerX64)
            {
                speed = 10;
            }
            else
            {
                speed = 5;
            }

        }

        currPosition.x += Mathf.Clamp(inputMovement.x, -1, 1) * Time.deltaTime * speed;

        Vector2 newPos = currPosition;
        newPos.x = Mathf.Clamp(currPosition.x, minBounds.x, maxBounds.x);

        transform.position = newPos;

    }

    public void OnFire()
    {

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
