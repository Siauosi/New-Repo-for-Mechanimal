using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHealthScript : MonoBehaviour
{
    public float playerHealth = 10;
    public float maxPlayerHealth = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0f)//if health drops below 0, mech death :P
        {


            Debug.Log("Mech Died :(");


        }

        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        
        }
    }

    public void PlayerTakeDamage(float damageTaken)
    {

        playerHealth -= damageTaken;//minuses damageTaken variable from the playerHealth variable
    
    }
}
