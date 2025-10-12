using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{

    public PlayerBaseJumpScript playerBaseJumpScript;

    public float speed;
    public float rotationSpeed; //speed in degrees per second
    public Animator legAnimator;
    public int speedMultiplier;
    public PlayerInputActions playerControls;
    private InputAction moveInputAction;
    private InputAction fire;
    Vector2 moveDirection = Vector2.zero;
    public Camera mechCam;
    




    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void Awake()
    {
        playerControls = new PlayerInputActions();


    }

    private void OnEnable()
    {
        moveInputAction = playerControls.Player.Move;
        moveInputAction.Enable();

        fire = playerControls.Player.Fire;  
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {

        moveInputAction.Disable();


        fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {

        Debug.Log("WeFired");
    }

    // Update is called once per frame
    void Update()
    {
        //obtain the movement direction based on the moveInputAction variable
        moveDirection = moveInputAction.ReadValue<Vector2>();
        //Debug.Log("moveDirection is" + moveDirection);
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 6;
            legAnimator.SetFloat("Speed", speedMultiplier);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 6;
            legAnimator.SetFloat("Speed", 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(playerBaseJumpScript.ExampleCoroutine());

        }
    }

    private void MovePlayer()
    {
        //obtain the camera's forward and right vectors
        Vector3 cameraForward = mechCam.transform.forward;
        Vector3 cameraRight = mechCam.transform.right;

        //remove vertical components to prevent the mech from moving up or down based on the camera's rotation
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        
        //calculate the movement based on the camera perspective
            Vector3 movement = cameraForward * moveDirection.y + cameraRight * moveDirection.x;
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
               legAnimator.SetBool("IsMoving", true);
           }
            else 
            { 
                legAnimator.SetBool("IsMoving", false);
            }
            transform.Translate(movement * speed * Time.deltaTime, Space.World);


        //mechRb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    
    }




}
