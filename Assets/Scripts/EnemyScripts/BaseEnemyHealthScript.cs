using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BaseEnemyHealthScript : MonoBehaviour


    {
        public MechControlScript mechController;
        public float enemyHealth;
        // Start is called before the first frame update
        void Start()
        {
            mechController = GameObject.Find("PlayerMech").GetComponent<MechControlScript>();
        }

        // Update is called once per frame
        void Update()
        {
            if (enemyHealth <= 0f)//if health drops below 0, destroy self.
            {

                mechController.AddEnemyDefeatedPoint();
                
                Destroy(gameObject);


            }
        }

        public void TakeDamage(float damage)//take 1 away from health
        {

            enemyHealth -= damage;


        }



    }



