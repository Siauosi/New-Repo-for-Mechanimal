using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace enemy
{

    public class BaseFloorEnemyScript : EnemyScript
    {
        public Transform currentTargetInVision;//creates a currentTargetinVision transform variable
        [SerializeField] protected float visionRadius = 10f;//the distance around the enemy, in which the enemy can see
        [SerializeField] protected float viewAngle = 90f;//the view angle that the enemy can see
        public LayerMask targetMask;//creates a targetMask variable
        public LayerMask obstructionMask;//creates a mask for obstructions


        public GameObject playerObject;


        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            targetMask = LayerMask.GetMask("Player");//assigns the targetMask to the Player layermask
            obstructionMask = LayerMask.GetMask("ObstructionLayer");//assigns the obstructionMask to the Obstruction layermask
            playerObject = GameObject.Find("PlayerMech");
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();//calls the base update

            SeeTargets();//calls the SeeTargets function

            if (overrideMovements == true)
            {
                var navMeshAgent = GetComponent<NavMeshAgent>();

                if (navMeshAgent != null)
                {
                    navMeshAgent.SetDestination(playerObject.transform.position);
                    overrideMovements = true;
                }


            }
        }



        public void SeeTargets()
        {

            {

                currentTargetInVision = null;//targetinvision variable is reset to null

                Collider[] targetsInVision = Physics.OverlapSphere(transform.position, visionRadius, targetMask);//creates a list for targets in vision



                foreach (var target in targetsInVision)//for the amount of every target in vision
                {
                    Vector3 directionToTarget = (target.transform.position - transform.position).normalized;//gets the direction of target from my/this position

                    float angleDifference = Vector3.Angle(transform.forward, directionToTarget);

                    float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);


                    if (angleDifference < viewAngle / 2f)
                    {


                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))//if there are no obstructions between myself and the target.
                        {
                            currentTargetInVision = target.transform;//the currenttargetinvision variable is set to the target found
                            //Debug.Log("Target within vision");
                            randomMovementActive = false;
                            StartCoroutine(RandomMovementRestartDelay(5f));//waits before restarting the random movements again

                            break;//stops after first target is found

                        }


                    }

                }







            }



        }

        public void MoveToPlayer()
        {
            if (GetComponent<NavMeshAgent>() != null)
            {
                var navMeshAgent = GetComponent<NavMeshAgent>();

                if (navMeshAgent != null && !overrideMovements)
                {
                    navMeshAgent.SetDestination(playerObject.transform.position);
                    overrideMovements = true;
                }
            }
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


        }
    }




}