using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2D : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float smoothing = 5.0f;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, (smoothing * 0.001f));
        }

        //Quaternion newRot = target.rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRot, (smoothing * 0.001f));
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
