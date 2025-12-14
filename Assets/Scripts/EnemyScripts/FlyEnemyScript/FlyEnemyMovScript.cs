using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyEnemyMovScript : MonoBehaviour
{
    public Transform playerTransform;

    private SphereCollider sphereCollider;
    public BoxCollider movementBounds;
    [SerializeField] float speed, secondsBetweenMovements;
    [SerializeField] float secondsBetweenDodges;

    //[SerializeField] float minMovementAreaZ, maxMovementAreaZ; //setting the variables for movement area range
    //[SerializeField] float minMovementAreaX, maxMovementAreaX; //setting the variables for movement area range
    //[SerializeField] float minMovementAreaY, maxMovementAreaY;
    public Vector3 movementPosition;
    private float movementRandomRangeZ;
    private float movementRandomRangeX;
    private float movementRandomRangeY;
    public bool movementPositionAchieved = true;
    public bool dodgeActive = true;
    public bool isDodging = false;

    public float sightRadius;
    public LayerMask playerLayer;

    public bool alive = true;
    public bool playerInSight = false;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PestMovement());
        playerTransform = GameObject.Find("PlayerMech").transform;
    }

    IEnumerator PestMovement()
    {

        while (true)//infinite loop till broken
        {
            if (!isDodging)
            {
                speed = 30;
                Bounds bounds = movementBounds.bounds;
                movementRandomRangeZ = Random.Range(bounds.min.z, bounds.max.z);//chooses a random movement value between the 2 variables for z
                movementRandomRangeX = Random.Range(bounds.min.x, bounds.max.x);//chooses a random movement value between the 2 variables for x
                movementRandomRangeY = Random.Range(bounds.min.y, bounds.max.y);//chooses a random movement value between the 2 variables for y
                movementPosition = new Vector3(movementRandomRangeX, movementRandomRangeY, movementRandomRangeZ);//finds a random point using the x and z random movement values
                


            }

            yield return new WaitForSeconds(secondsBetweenMovements);
        }









    }

    IEnumerator PestDodgeCooldownReset()
    {

        yield return new WaitForSeconds(secondsBetweenDodges);
        dodgeActive = true;
        Debug.Log("Dodge Off Cooldown");
        isDodging = false;









    }


    // Update is called once per frame
    void Update()
    {

        // if (pestHealthScript.dead == true && alive == true)
        // {

        //     alive = false;

        //     Debug.Log("Pest deaded.");

        // }

        if ((alive))
        {
            
            transform.position = Vector3.MoveTowards(transform.position, movementPosition, speed * Time.deltaTime);//moves towards the random position at a set speed within the inspector.

            Bounds bounds = movementBounds.bounds;
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x, bounds.max.x);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, bounds.min.z, bounds.max.z);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, bounds.min.y, bounds.max.y);
            transform.position = clampedPosition;
        }

       
        
        
        playerInSight = Physics.CheckSphere(transform.position, sightRadius, playerLayer);

        
        
            if (playerInSight)
            {
                Vector3 direction = playerTransform.position - transform.position; //finds the direction of player

                transform.rotation = Quaternion.LookRotation(direction);//rotates the flyenemy towards the player
            }
        
    }


    public void Dodge()
    {
        if (dodgeActive == true)
        {
            Bounds bounds = movementBounds.bounds;
            speed = 100;
            Bounds movementBoundsBox = movementBounds.bounds;
            movementRandomRangeZ = Random.Range(bounds.min.z, bounds.max.z);//chooses a random movement value between the 2 variables for z
            movementRandomRangeX = Random.Range(bounds.min.x, bounds.max.x);//chooses a random movement value between the 2 variables for x
            movementRandomRangeY = Random.Range(bounds.min.y, bounds.max.y);//chooses a random movement value between the 2 variables for y
            movementPosition = new Vector3(movementRandomRangeX, movementRandomRangeY, movementRandomRangeZ);//finds a random point using the x and z random movement values

            dodgeActive = false;

            StartCoroutine(PestDodgeCooldownReset());
        }

    }


    void OnDrawGizmosSelected()
    {
        // Set the gizmo color (e.g., green for the check area)
        Gizmos.color = Color.cyan;

        // Draw the wireframe sphere to visualize the check radius
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}

