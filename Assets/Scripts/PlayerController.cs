using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private Vector3 playerPos;

    private float inputMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerPos = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnEnable()
    {
        //TODO Fix this code

       // playerInput.Enable();
    }

    private void OnDisable()
    {
       // playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();

    }

    //Move the Player

    public void playerMove()
    {
        Vector3 currPosition = transform.position;
        currPosition.x += inputMovement * Time.deltaTime * 5f;
        transform.position = currPosition;
     }

    //Start Input functions

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputMovement = context.ReadValue<float>();


        }
    }
}
