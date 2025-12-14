using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{
    public MechHealthScript mechHealthScript;
    public GameObject playerMechHealthObj;
    public float healAmount = 5f;

    public void Start()
    {
        playerMechHealthObj = GameObject.Find("BoxColliderMech");
        mechHealthScript = playerMechHealthObj.GetComponent<MechHealthScript>();
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            mechHealthScript.playerHealth += healAmount;
            Destroy(gameObject, 0.2f);//destroy object after 0.2 seconds.
        }
    }



}

