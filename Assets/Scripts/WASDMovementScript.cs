using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WASDMovementScript : MonoBehaviour
{
    public PlayerBaseJumpScript jumpAnimatorScript;

    public float rotationSpeed; //speed in degrees per second
    public Animator legAnimator;
    public int speedMultiplier;
    public float speed = 0.5f;
    public Camera mechCam;

    // gravity and jump variables
    public bool jumpLanded;
    public bool jumpNow;
    public float forwardJumpSpeedAmount;
    public float currentForwardJumpSpeed =0f;
    public float jumpHeight;
    public float gravityScale = 5;
    public float velocity;
    public float maxDistance = 4.66f;


    public bool isGrounded;
    public LayerMask floorMask;
    public LayerMask wallMask;
    public Ray groundedRay;

    RaycastHit hit;

    //StaminaUI Variables

    public Image staminaBar;

    public float stamina, maxStamina;

    public float sprintCost;

    public float rechargeRate;

    public bool sprinting;

    private Coroutine staminaRecharge;

    public float walkSpeed, sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        groundedRay = new Ray(transform.position, -transform.up);

        isGrounded = (Physics.Raycast(groundedRay, out hit, maxDistance, floorMask));
       


        velocity += Physics.gravity.y * gravityScale * Time.deltaTime;//downward force using gravity scale 

        if (jumpNow && isGrounded)
        {
            currentForwardJumpSpeed = forwardJumpSpeedAmount;
            velocity = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * gravityScale));//jump with positive force  applied gravity
            jumpNow = false;
            


        }

        //if mech clips into ground, reset the distance to the maxDistance.
        if ((transform.position - hit.point).magnitude <= maxDistance)
        {
            transform.position = hit.point + new Vector3(0, maxDistance, 0);
        }


        //grounded checks for animator
        if (isGrounded)
        {
            legAnimator.SetBool("IsGrounded", true);
        }
        else 
        {
            legAnimator.SetBool("IsGrounded", false);
        }


        if (isGrounded && velocity <0)//if grounded and moving downard, reset velocity and currentFJumpS to 0
        {
            velocity = 0;
            currentForwardJumpSpeed = 0;

            if (jumpLanded)//if grounded after a jump
            {
                StartCoroutine(jumpAnimatorScript.JumpLandedCoroutine());
                jumpLanded = false;

            }

        }

        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);//apply downward force of gravity, * by time again, always
        transform.Translate(new Vector3(0, 0, currentForwardJumpSpeed) * Time.deltaTime);//apply forward force of jump
        //------------------------------------------------
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 cameraForward = mechCam.transform.forward;
        Vector3 cameraRight = mechCam.transform.right;

        //remove vertical components to prevent the mech from moving up or down based on the camera's rotation
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();


        //calculate the movement based on the camera perspective
        Vector3 moveDirection = cameraForward * zDirection + cameraRight * xDirection;

        //Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

        if (!jumpAnimatorScript.currentlyJumping)//if not jumping
        {
            transform.position += moveDirection * speed;//movement
        }
        if (moveDirection != Vector3.zero && !jumpAnimatorScript.currentlyJumping)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
            legAnimator.SetBool("IsMoving", true);
        }
        else
        {
            legAnimator.SetBool("IsMoving", false);
        }




        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            sprinting = true;


        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;


        }
        if (stamina <= 0)
        {
            sprinting = false;
        }


        if (sprinting)
        {
            Debug.Log("Sprint Initiated");

            speed = sprintSpeed;
            legAnimator.SetFloat("Speed", speedMultiplier);

            stamina -= sprintCost * Time.deltaTime;//stamina is drained by the sprintcost every second
            if (stamina < 0) stamina = 0;
            staminaBar.fillAmount = stamina / maxStamina;//the bar fill is the stamina divided by the max stamina amount

            if (staminaRecharge != null) StopCoroutine(staminaRecharge);//stop stamina recharge
            staminaRecharge = StartCoroutine(RechargeStamina());//start stamina recharge
        }
        else 
        {
            speed = walkSpeed;
            legAnimator.SetFloat("Speed", 1f);
        }

    }


    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (stamina < maxStamina)//while the stamina is less than the max stamina, do the stamina recharging code
        {
            stamina += rechargeRate / 10f;
            if(stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    
    }

}
