using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public BaseEnemyScript enemyScript;
    public GameObject enemyObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//shoot if there is ammunition
        {



            if (collision.gameObject.CompareTag("EnemyTag"))
            {
                enemyObject = collision.gameObject;
                enemyScript = enemyObject.GetComponent<BaseEnemyScript>();
                enemyScript.TakeDamage();
                Debug.Log("Melee Hit Enemy");


            }
        }

        if (collision.gameObject.CompareTag("EnemyTag"))
        {
            enemyObject = collision.gameObject;
            enemyScript = enemyObject.GetComponent<BaseEnemyScript>();
            enemyScript.TakeDamage();
            Debug.Log("Melee Hit Enemy");


        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//shoot if there is ammunition
        {



            if (collision.gameObject.CompareTag("EnemyTag"))
            {
                enemyObject = collision.gameObject;
                enemyScript = enemyObject.GetComponent<BaseEnemyScript>();
                enemyScript.TakeDamage();
                Debug.Log("Melee Hit Enemy");


            }
        }
    }
}
