using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseJumpScript : MonoBehaviour
{
    public Animator legAnimator;
    public Transform mechTransform;

    public WASDMovementScript wasdMovementScript;

    public Image cooldownBar;

    public float cooldown, maxCooldown;

    public float cooldownCost;

    public float rechargeRate;

    public bool currentlyJumping;

    private Coroutine jumpRecharge;

    // Start is called before the first frame update
    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        //StartCoroutine(ExampleCoroutine());
        currentlyJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !currentlyJumping && cooldown >=300)
        {

            StartCoroutine(ExampleCoroutine());
            Debug.Log("Space is pressed");
            currentlyJumping = true;
            cooldown -= cooldownCost;
            cooldownBar.fillAmount = cooldown / maxCooldown;
        }

        if (currentlyJumping)
        {

            if (jumpRecharge != null) StopCoroutine(jumpRecharge);//stop jump recharge
            jumpRecharge = StartCoroutine(RechargeStamina());//start jump recharge
        }
    }

    public void JumpCharge()
    {
        legAnimator.SetBool("IsChargingJump", true);
    }



    public void JumpAir()
    {
        legAnimator.SetBool("IsInJumpAir", true);

        
    }



    public void JumpLand()
    {
        legAnimator.SetBool("IsJumpLanding", true);
        legAnimator.SetBool("IsChargingJump", false);
        legAnimator.SetBool("IsInJumpAir", false);
    }

    public void JumpFinished()
    {
        legAnimator.SetBool("IsJumpLanding", false);
        currentlyJumping = false;
    }

    public IEnumerator ExampleCoroutine()
    {
        JumpCharge();
        //Print the time of when the function is first called.
        Debug.Log("Charging : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Jumping : " + Time.time);
        wasdMovementScript.jumpNow = true;
        JumpAir();
        wasdMovementScript.jumpLanded = true;




        
    }
public IEnumerator JumpLandedCoroutine()
    {
        Debug.Log("Landing : " + Time.time);
        JumpLand();

        yield return new WaitForSeconds(1.3f);

        Debug.Log("Landing Finished : " + Time.time);
        JumpFinished();
    }


    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (cooldown < maxCooldown)//while the stamina is less than the max stamina, do the stamina recharging code
        {
            cooldown += rechargeRate / 10f;
            if (cooldown > maxCooldown) cooldown = maxCooldown;
            cooldownBar.fillAmount = cooldown / maxCooldown;
            yield return new WaitForSeconds(.1f);
        }

    }
}
