using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseJumpScript : MonoBehaviour
{
    public Animator legAnimator;
    public Transform mechTransform;

    public WASDMovementScript wasdMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        //StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(ExampleCoroutine());
            Debug.Log("Space is pressed");
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
}
