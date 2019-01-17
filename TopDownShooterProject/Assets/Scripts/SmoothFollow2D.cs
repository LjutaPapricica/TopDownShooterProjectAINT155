using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2D : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float smoothing = 5.0f;
    public Vector3 mousePos;

    private void FixedUpdate()
    {
        //if the player transform is known to the camera
        if (target != null)
        {
            //gets vector3 position of the mose cursor in the world
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //midpoint between player tank and mouse position calculated
            Vector3 midPoint = (target.position + mousePos) / 2;

            //new position with midpoint x and y position created
            Vector3 newPos = new Vector3(midPoint.x, midPoint.y, transform.position.z);
            //camera changes position smoothly toward midpoint each update
            transform.position = Vector3.Lerp(transform.position, newPos, (smoothing * 0.001f));
        }

        //OLD CODE
        //Quaternion newRot = target.rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRot, (smoothing * 0.001f));
    }

    //sets new target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
