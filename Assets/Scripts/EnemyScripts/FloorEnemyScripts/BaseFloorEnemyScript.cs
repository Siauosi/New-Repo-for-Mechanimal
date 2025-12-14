using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace enemy

{
    public class EnemyScript : MonoBehaviour
    {
        public float health = 10f;//health variable

        public bool overrideMovements;

        public bool randomMovementActive = true;//variable sees whether or not the enemy is moving to somehwere
        public NavMeshAgent enemyAgent;//the navmesh agent variable
        public float randomMovementRadius = 15f;//the radius in which the enemy can create a point to move toward
        public float randomMovementTimer = 3f;//how long the wait is before movement points changed

        [SerializeField] private float timer;

        // Start is called before the first frame update
        public virtual void Start()
        {
            if (enemyAgent == null)//if the enemyagent doesnt have a variable attached
            {
                enemyAgent = GetComponent<NavMeshAgent>();//adds the navmesh agent variable to the enemyAgent variable
            }

            timer = randomMovementTimer;//sets the timer to be the same as randomMovementTimer



        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (randomMovementActive == true && !overrideMovements)
            {
                timer += Time.deltaTime;//timer is set to time

                if (timer >= randomMovementTimer)//if the timer is more than the randomMovementTimer do the next lines of code
                {
                    Vector3 newMovementPosition = GetRandomNavMeshMovementPosition(transform.position, randomMovementRadius);//finds a new movement position
                    enemyAgent.SetDestination(newMovementPosition);
                    timer = 0f;
                }

            }

            if (health <= 0f)//if health drops below 0, destroy self.
            {


                Destroy(gameObject);


            }
        }

        public IEnumerator RandomMovementRestartDelay(float delay)//wait 5 seconds before going back to random movements and returning the speed to normal
        {
            yield return new WaitForSeconds(delay);

            randomMovementActive = true;
            if (enemyAgent != null)
            {
                enemyAgent.speed = 5f;
            }
        }

        Vector3 GetRandomNavMeshMovementPosition(Vector3 center, float radius)//this function calculates a random position within the movementradius
        {
            if (randomMovementActive == true)
            {
                for (int i = 0; i < 30; i++)//try 30times.
                {

                    Vector3 randomMovementPosition = center + Random.insideUnitSphere * radius;//finds a random point inside a sphere that is multiplied by the radius variable.
                    if (NavMesh.SamplePosition(randomMovementPosition, out NavMeshHit hit, 3f, NavMesh.AllAreas))//if the random position is in the 3f range on any of the navmesh areas
                    {
                        return hit.position;//return the random position

                    }


                }
            }
            return transform.position;//if no random position found, return the initial transform's position


        }


       // public void TakeDamage(float damage)//take 1 away from health
       // {

       //     health -= damage;
       //     Debug.Log($"Applying {damage} damage");

       // }



    }

}
