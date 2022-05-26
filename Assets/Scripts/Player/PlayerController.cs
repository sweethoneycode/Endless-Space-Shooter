using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamagable
{
    public PlayerInput playerInput;
    private Vector3 playerPos;

    private Vector2 inputMovement;
    public float fireAction;
    private readonly float firingCooldown = 0.4f;
    private float cooldownTimer;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    [SerializeField] private float playerHealth = 10f;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip playHit;

    public GameObject explosionPrefab;

    [SerializeField] private bool projectileEnabled = true;

    //Set by GameSceneController
    [SerializeField] public float speed;

    [SerializeField] private GameObject PlayerWeapon;

    [SerializeField] private AudioClip PlayerHit;
    [SerializeField] private AudioClip PlayerExplode;
    [SerializeField] private AudioClip shieldAudio;

    [SerializeField] private HealthBarBehavior HealthBarBehavior;

    [SerializeField] private float CurrentShields { get; set; }

    private bool restoreShields = true;
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
        HealthBarBehavior.fixedPosition = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y, 0);
        EventBroker.ProjectileOutOfBounds += EnableProjectile;
        EventBroker.RestoreShields += RestoreShields;

        RestoreShields();

        HealthBarBehavior.SetHealth(playerHealth, 10f);
        
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

    private void RestoreShields()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(shieldAudio);

        playerHealth = 10f;
        HealthBarBehavior.RestoreHealth();
    }

    public void ChangeHealth(float enemyHealth)
    {
        HealthBarBehavior.SetHealth(enemyHealth, 10f);
    }

    // Update is called once per frame
    void Update()
    {

        playerInput.Player.Pause.performed += Pause_performed;
        if(PlayerWeapon)
            PlayerWeapon.transform.position = transform.position;
        //player move event


        //fire event
        if (projectileEnabled)
        {
            playerMove();
            OnFire();
        }


        ChangeHealth(playerHealth);

    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {

        EventBroker.CallPauseGame();

    }

    private IEnumerator PlayerDeath()
    {

        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        EventBroker.CallPlayerDeath();


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

        currPosition.x -= Mathf.Clamp(inputMovement.x, -1, 1) * Time.deltaTime * speed;

        Vector2 newPos = currPosition;
        newPos.x = Mathf.Clamp(currPosition.x, minBounds.x, maxBounds.x);

        transform.position = newPos;

    }

    private void CreateWeapon()
    {
      //  ChooseWeapon.pickWeapon(10f, tag);

    }

    public void OnFire()
    {

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = firingCooldown;

            CreateWeapon();

        }


    }

    public void Damage(float lazorDamage, string lazorTag)
    {
        if (lazorTag != tag)
        {

            if (playerHealth > 0)
            {
                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySFX(PlayerHit);

                playerHealth -= lazorDamage;


            }

            if (playerHealth <= 0)
            {
                playerHealth = 0;

                animator.SetBool("PlayerDeath", true);

                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySFX(PlayerExplode);


                StartCoroutine(PlayerDeath());
            }

            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.position = transform.position;
            Destroy(explosionInstance, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "weapon")
        {
            Debug.Log("Hit weapon");
            PlayerWeapon = collision.gameObject;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PlayerWeapon.GetComponentInChildren<SpriteRenderer>().enabled = false;
            PlayerWeapon.GetComponent<ChooseWeapon>().pickWeapon(10f, tag);
        }

    }
}
