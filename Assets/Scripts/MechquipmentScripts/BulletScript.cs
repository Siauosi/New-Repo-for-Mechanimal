using System.Collections;
using System.Collections.Generic;
using enemy;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletDamage;
    public BaseFloorEnemyScript baseFloorEnemyScript;
    public BaseEnemyHealthScript enemyScript;
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
            enemyScript = enemyObject.GetComponent<BaseEnemyHealthScript>();
            enemyScript.TakeDamage(bulletDamage);
            Debug.Log("Bullet Hit Enemy");

            
            //baseFloorEnemyScript = enemyObject.GetComponent<BaseFloorEnemyScript>();
            //baseFloorEnemyScript.TakeDamage(bulletDamage);
            //Debug.Log("Bullet Hit Floor Enemy");
            

            Destroy(gameObject);

        }




    }



}
