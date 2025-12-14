using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTabletScript : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public int spawnCount = 3;
    public float spawnPeriod = 15;
    public float timeBetweenSpawns;

    public bool allowSpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (allowSpawn == true)
        {
            StartCoroutine(ProceduralEnemySpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ProceduralEnemySpawn()
    {
        
        timeBetweenSpawns = spawnPeriod / spawnCount;

        for (int i = 0; i < spawnCount; i++)
        { 
        
            Instantiate(prefab,spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    
    }
}
