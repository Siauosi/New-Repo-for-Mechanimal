using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NewLevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemySpawnTabletScript enemySpawnScript;

    [SerializeField] GameObject[] spawnTablets;
    private bool spawnTabletsActivated = false;
     
    private void OnTriggerEnter(Collider other)
    {
        if (!spawnTabletsActivated && other.CompareTag("Player"))
        {
            spawnTabletsActivated=true;


            foreach (GameObject tablet in spawnTablets)
            {
                tablet.SetActive(true);


                EnemySpawnTabletScript spawnScript = tablet.GetComponent<EnemySpawnTabletScript>();
                if (spawnScript != null)
                {

                    spawnScript.spawnCount++;//increases spawn count
                    spawnScript.allowSpawn = true;//allows the spawn coroutine to occur

                }
            }
            

            Destroy(gameObject);


        }
    }
}
