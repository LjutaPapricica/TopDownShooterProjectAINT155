using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsObject : MonoBehaviour {

    public Transform target;
    public float speed = 5.0f;

    private void Update()
    {

        if (target != null)
        {
            Vector3 currentPos = transform.position;
            Vector3 targetPos = target.position;

            float dist = Vector3.Distance(transform.position, target.position);

            transform.position = Vector3.MoveTowards(currentPos, targetPos, speed * 0.01f);
        }

        

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
