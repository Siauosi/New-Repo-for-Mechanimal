using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionTransformScript : MonoBehaviour
{
    public Camera mainCamera;

    //layermasks to make contact for mouse position
    public LayerMask floorLayer;
    public LayerMask enemyLayer;
    public LayerMask wallLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);//creates a ray from the camera to the mouse position

        LayerMask combineHitMask = floorLayer | enemyLayer| wallLayer;//create a combined layermask

        //find the hitpoint based on the ray and set raycast layer.
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, combineHitMask))
        {
            //sets the position of the transform to the position of the intersection.
            transform.position = hit.point;
        }
    }
}
