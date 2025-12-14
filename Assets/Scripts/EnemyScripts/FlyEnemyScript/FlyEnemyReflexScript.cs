using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyReflexScript : MonoBehaviour
{
    public LayerMask bulletLayer;
    public FlyEnemyMovScript fEnemyMScript;
    public bool bulletInReflexSphere = false;
    public float reflexRange;
    // Start is called before the first frame update

    //public void Start()
    //{
    //fEnemyMScript = GetComponent<FlyEnemyMovScript>();
    //}
    private void Start()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletTag"))
        {

            fEnemyMScript.Dodge();

            Debug.Log("bullet collided with reflex sphere");

        }
    }


    private void Update()
    {
        bulletInReflexSphere = Physics.CheckSphere(transform.position, reflexRange, bulletLayer);

        if (bulletInReflexSphere && fEnemyMScript.dodgeActive)
        {
            fEnemyMScript.Dodge();
            Debug.Log("bullet collided with reflex check sphere");
        }
        
            
        
    }

    void OnDrawGizmosSelected()
    {
        // Set the gizmo color (e.g., green for the check area)
        Gizmos.color = Color.green;

        // Draw the wireframe sphere to visualize the check radius
        Gizmos.DrawWireSphere(transform.position, reflexRange);
    }
    }
