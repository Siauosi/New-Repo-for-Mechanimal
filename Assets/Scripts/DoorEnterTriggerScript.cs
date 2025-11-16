using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnterTriggerScript : MonoBehaviour
{
    public Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("DoorOpenBool", true);

            Debug.Log("Detected Player");
        }
    }
}
