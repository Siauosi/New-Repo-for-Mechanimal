using System.Collections;
using System.Collections.Generic;
using enemy;
using gun;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public BaseEnemyHealthScript enemyHealthScript;
    public GameObject enemyObject;


    public float meleeRange;
    public LayerMask enemyLayer;
    public float meleeDamage;
    public bool meleeHasHit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//shoot if there is ammunition
        {

            MeleeAttack();


        }


    }

    public void MeleeAttack()
    {
        meleeHasHit = false;

        Collider[] meleehits = Physics.OverlapSphere(transform.position, meleeRange, enemyLayer);

        foreach (Collider meleehit in meleehits)
        {

            if (meleeHasHit) break;

            enemyHealthScript = meleehit.GetComponent<BaseEnemyHealthScript>();
            if (enemyHealthScript != null)
            { 
                enemyHealthScript.TakeDamage(meleeDamage);
            }
        }
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

}

    

