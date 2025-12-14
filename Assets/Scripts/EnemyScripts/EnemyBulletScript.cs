using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject mechObject;
    public MechHealthScript mechHealthScript;
    public float bulletDamage;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            mechObject = collider.gameObject;
            mechHealthScript = mechObject.GetComponent<MechHealthScript>();
            mechHealthScript.PlayerTakeDamage(bulletDamage);
            Debug.Log("Enemy bullet hit player");

            Destroy(gameObject);

        }




    }

}
