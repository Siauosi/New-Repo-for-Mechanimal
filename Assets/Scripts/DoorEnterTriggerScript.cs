using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnterTriggerScript : MonoBehaviour
{
    public MechControlScript mechController;
    public Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mechController = GameObject.Find("PlayerMech").GetComponent<MechControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mechController.enemiesDefeated == 11)
        {
            doorAnimator.SetBool("DoorOpenBool", true);

        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        doorAnimator.SetBool("DoorOpenBool", true);
//
    //        Debug.Log("Detected Player");
     //   }
    //}
}
