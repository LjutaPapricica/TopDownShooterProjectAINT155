using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookAtTarget2D : MonoBehaviour {
    
    public Transform target;
    public float smoothing = 5.0f;
    public float adjustmentAngle = 0.0f;

    private void Update()
    {
        //prevents null exception if no target assigned
        if (target != null)
        {
            //difference between position vectors of this gameobject and the target transform calculcated
            Vector3 difference = target.position - transform.position;

            //rotation in z axis between the two points calculated
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            //adjustment angle added if sprite facing wrong direction
            Quaternion newRot = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotZ + adjustmentAngle));

            //smooth transition when gameobject is turning on each update() call
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * smoothing);            

        }

    }
    //target transform can be set using this method
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
