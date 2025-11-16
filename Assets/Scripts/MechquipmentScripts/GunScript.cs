using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace gun
{
    public class GunScript : MonoBehaviour
    {
        public MousePositionTransformScript mousePosScript;

        public Transform targetPoint;
        public Transform firePoint;
        public GameObject bullet;
        public GameObject fireEffect;
        public float shootForce;

        [Header("Ammunition and Reload Variables")]
        public int maxAmmunition = 10;
        public int currentAmmunition;
        public int spareAmmunition = 100;
        public float reloadTime = 2f;
        public bool isReloading = false;

        // Start is called before the first frame update
        public virtual void Start()
        {
            spareAmmunition = 100;

            currentAmmunition = maxAmmunition;

            shootForce = 100f;//this will be the force applied to the bullet.

            targetPoint = mousePosScript.transform;//sets the playerCamera variable to the maincamera within the scene






            foreach (Transform t in GetComponentsInChildren<Transform>())//this finds the firePoint by going down the list of child transforms and identifying the one with name "firePoint"
            {
                if (t.name == "firePoint")
                {
                    firePoint = t;
                    break;
                }
            }

            if (firePoint == null)
            {
                Debug.LogWarning("firePoint not found.");//if there isnt a firepoint found a debug warning will show.
            }
        }



        // Update is called once per frame
        void Update()
        {
            if (isReloading) return;

            if (Input.GetKeyDown(KeyCode.Mouse1) && currentAmmunition >= 1)//shoot if there is ammunition
            {
                Shooting();

            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentAmmunition < maxAmmunition && spareAmmunition > 0)//reload if bullets are reduced and spare ammunition is available
                {
                    StartCoroutine(Reload());
                }


            }





        }

        public IEnumerator Reload()
        {
            isReloading = true;//sets is reloading to true
            Debug.Log("Reloading Ammo..");//debugs reloading
            yield return new WaitForSeconds(reloadTime);//waits for the reloadTime variable

            int ammunitionNeeded = maxAmmunition - currentAmmunition;//calculates required ammunition, by minusing the maxAmmunition from the current ammunition
            int ammunitionToReload = Mathf.Min(ammunitionNeeded, spareAmmunition);//calculates the ammunition to reload by obtaining the smaller variable, cant take more than you have.

            currentAmmunition += ammunitionToReload;//adds the needed ammunition for reload
            spareAmmunition -= ammunitionToReload;//takes away from the spare ammunition

            Debug.Log("Ammo should be ready!");
            isReloading = false;


        }

        public virtual void Shooting()
        {
            currentAmmunition -= 1;

            
            Vector3 direction = (targetPoint.position - firePoint.position).normalized ; //sets a local variable of direction.
            RaycastHit hit; //creates a local RaycastHit variable to be used later lines.

            if (Physics.Raycast(firePoint.position, direction, out hit, 100)) //if the raycast sent hits something within 100 units do the next lines of code
            {
                Debug.Log("Shot with target"); //debug for clarification
                Debug.DrawRay(firePoint.position, direction * hit.distance, Color.yellow);//draws a ray that can be scene within the editing scene between firepoint and hitpoint

                GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity); //currentbullet variable added to allow for access to the instantiations
                currentBullet.GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Impulse); //applying the shootforce to the bullet in a forward direction
                Destroy(currentBullet, 6f); //destroy the instantiated bullet after 6 seconds

                var currentFireEffect = Instantiate(fireEffect, firePoint.position, firePoint.rotation);//Instantiates an effect at the hitpoint position
                Destroy(currentFireEffect, 2f); //destroys the effect after 2 seconds
                //Instantiate(hitPointEffect, hit.point, Quaternion.identity);

            }

            else //if the raycast sent doesnt hit something within 100 units do the next lines of code
            {
                Debug.Log("Shot with no target"); //debug for clarification


                GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity); //currentbullet variable added to allow for access to the instantiations
                currentBullet.GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Impulse); //applying the shootforce to the bullet in a forward direction
                Destroy(currentBullet, 6f); //destroy the instantiated bullet after 6 seconds

                var currentFireEffect = Instantiate(fireEffect, firePoint.position, firePoint.rotation);//Instantiates an effect at the hitpoint position
                Destroy(currentFireEffect, 2f); //destroys the effect after 2 seconds
                //Instantiate(hitPointEffect, hit.point, Quaternion.identity);

            }


        }

        public virtual void CancelReload()//this is called when a weapon is changed, so if a weapon is reloading, it cancels the reload
        {
            if (isReloading)
            {
                StopAllCoroutines();//stops all coroutines
                isReloading = false;//resets reloading to false

                Debug.Log("Reloading cancelled.");
            }
        }
    }
}
