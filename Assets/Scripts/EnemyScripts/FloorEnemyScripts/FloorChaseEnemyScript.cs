using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace enemy
{
    public class ChaserEnemyScript : BaseFloorEnemyScript
    {

        public MechHealthScript mechHealthScript;

        public float chaserDmg = 1f;
        public float chaseDistance = 15f;
        private bool chaseInProgress = false;
        private bool attackActive = true;
        private float attackWaitTime = 1f;

        public bool playerInAttackRange = false;
        public float attackRange;
        public bool attackNotActivated = true;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();//calls the base start function

            visionRadius = 15f;//overrides the vision radius

            enemyAgent.angularSpeed = 240f;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if (currentTargetInVision != null)//if a target is within vision
            {

                MoveTowardTarget(currentTargetInVision);//move towards a target, specifically the target within vision

            }

            else if (!randomMovementActive && !chaseInProgress)//if there is no target in vision, a chase isnt in progress and random movements aren't active
            {

                StartCoroutine(RandomMovementRestartDelay(5f));//starts the random movement after 5 seconds.



            }

            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);

            if (playerInAttackRange && attackNotActivated && currentTargetInVision!= null)
            {
                StartCoroutine(AttackEnemy());
                attackNotActivated = false;

            }

           

        }


        protected void MoveTowardTarget(Transform target)
        {
            float distance = Vector3.Distance(transform.position, target.position);//gets the distance between target and enemy

            if (distance < chaseDistance)
            {

                chaseInProgress = true;//chase in progress is set to true
                enemyAgent.SetDestination(target.position);//enemy's destination is set to the position of the target.


            }
            else
            {
                chaseInProgress = false;//chase in progress is set to false

                enemyAgent.ResetPath();//enemy clears their current path


            }






        }

        //private void OnCollisionEnter(Collision collision)
        //{

        //    if (collision.gameObject.CompareTag("Player"))//if the target collided with is a player
        //    {

        //        playerObject = collision.gameObject;
        //        mechHealthScript = playerObject.GetComponent<MechHealthScript>();

        //        if (attackActive)
        //        {
        //           Debug.Log("Im hitting you with my mind!");
        //            StartCoroutine(AttackEnemy());
        //        }
        //    }


        //}

         public IEnumerator AttackEnemy()
        {
            Debug.Log("Attack coroutine initiated");

            attackActive = false;
            enemyAgent.isStopped = true; 



            yield return new WaitForSeconds(0.5f); //windup

            // Safety checks
            if (currentTargetInVision != null && currentTargetInVision.TryGetComponent(out MechHealthScript mechHealth))
            {
                mechHealth = currentTargetInVision.gameObject.GetComponentInParent<MechHealthScript>();
                mechHealth.PlayerTakeDamage(chaserDmg);
                Debug.Log("Player damaged by enemy");
            }
            else
            {
                Debug.LogWarning("No valid target to attack!");
            }

            yield return new WaitForSeconds(attackWaitTime); //cooldown

            enemyAgent.isStopped = false;
            enemyAgent.speed = 15f;
            attackActive = true;
            attackNotActivated = true; //re-enable attack
        }





        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;//gizmo colour will be blue
            Gizmos.DrawWireSphere(transform.position, visionRadius);//creates a gizmo of the vision radius as a sphere


            Vector3 forward = transform.forward; //a forward variable that is set to the transform's forward


            Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward * visionRadius;//creates a variable that will be the left line of the cone gizmo, using the viewAngle, to match it.

            Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * forward * visionRadius;//creates a variable that will be the right line of the cone gizmo, using the viewAngle, to match it.

            Gizmos.color = Color.yellow; //gizmo colour will be yellow
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);//draws the line using the previous variables
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);//draws the line using the previous variables




            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);


            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }



    }

}
