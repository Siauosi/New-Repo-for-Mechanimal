using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLookAtScript : MonoBehaviour
{
    public Transform target;
    public float minAngle;
    public float maxAngle;
    public float rotationSpeed; //rotation speed in degrees per second
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;//the direction equals the target's position minus this transform's position.

        Quaternion lookRotation = Quaternion.LookRotation(direction);//the rotation variable lookRotation is obtained using the LookRotation method.


        Vector3 euler = lookRotation.eulerAngles;//converts the look rotation to euler angle


        euler.x = NormalizeAngle(euler.x);//using the normalizeangle function

        euler.x = Mathf.Clamp(euler.x, minAngle, maxAngle);//clamps the x axis rotation between the min and max Angle.

        Quaternion clampedRotation = Quaternion.Euler(euler);//use the euler variable and convert it back to a quaternion, to be applied to the objects rotation


        transform.rotation = transform.rotation = Quaternion.RotateTowards(transform.rotation, clampedRotation, rotationSpeed * Time.deltaTime);//apply the clamped rotation and rotate based on the rotation speed
    }

    float NormalizeAngle(float angle)//a function to convert the angle to a range from -180 to 180 degrees
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
