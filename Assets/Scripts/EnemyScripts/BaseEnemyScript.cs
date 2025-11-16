using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    public float enemyHealth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0f)//if health drops below 0, destroy self.
        {


            Destroy(gameObject);


        }
    }

    public void TakeDamage()//take 1 away from health
    {

        enemyHealth -= 1;


    }


    private void OnTriggerEnter(Collider other)
    {





        if (other.gameObject.CompareTag("PlayerMelee"))
        {

            if (Input.GetKey(KeyCode.Mouse0))//shoot if there is ammunition
            {
                TakeDamage();

            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerMelee"))
        {

            if (Input.GetKey(KeyCode.Mouse0))//shoot if there is ammunition
            {
                TakeDamage();

            }

        }
    }
}

