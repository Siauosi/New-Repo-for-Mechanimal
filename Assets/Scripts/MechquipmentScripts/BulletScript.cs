using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
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
        if (collision.gameObject.CompareTag("EnemyTag"))
        {
            enemyObject = collision.gameObject;
            enemyScript = enemyObject.GetComponent<BaseEnemyScript>();
            enemyScript.TakeDamage();
            Debug.Log("Bullet Hit Enemy");

            Destroy(gameObject);

        }


    }



}
