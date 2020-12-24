using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private Vector3 playerPos;

    public GameObject missleBullet;
    private Rigidbody missleRB;

    private float inputMovement;
    public float fireAction;

    private bool isFiring = false;

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
        playerMove();
        OnFire();
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

        if (fireAction == 1)
        {
            if (isFiring == false) {
                isFiring = true;
                GameObject laserObject = Instantiate(missleBullet, transform.position, missleBullet.transform.rotation, transform.parent);
                missleRB = laserObject.GetComponent<Rigidbody>();
                missleRB.velocity = new Vector2(0, 3f);
                Destroy(laserObject, 5f);
            }
        }
        else
        {
            isFiring = false;
        }

    }
}
