using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    public FlyEnemyMovScript flyMovScript;

    
    public Transform player;
    public GameObject playerMechObj;
    public GameObject enemyBullet;
    public Transform firePoint;
    public float bulletSpeed;
    public float fireRate = 1.5f;

    private float nextShotTime;



    public void Start()
    {
        flyMovScript = GetComponent<FlyEnemyMovScript>();
        playerMechObj = GameObject.Find("PlayerMech");
        player = playerMechObj.transform;

    }
    void Update()
    {

        if (Time.time >= nextShotTime && flyMovScript.playerInSight)//time has to be past the shottime to shoot
        {
            Shoot();
            nextShotTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);//instantiates bullet at firepoint position

        Vector3 direction = (player.position - firePoint.position).normalized;//sets the direction towards the player
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;//applies velocity to the bullet with the set direction
    }
}
