using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFinder : MonoBehaviour {

    public Transform target;
    private IAstarAI ai;
    private AILerp aiLerp;
    private Rigidbody2D rb;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        aiLerp =  GetComponent<AILerp>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        

        if (target != null && ai != null)
        {
            ai.destination = target.position;
            ai.SearchPath();

            float dist = Vector3.Distance(transform.position, target.position);

            if (dist < 3)
            {
                aiLerp.canMove = false;
                rb.velocity = new Vector2(0f, 0f);
            }
            else if (dist > 6)
            {
                aiLerp.canMove = true;
            }
        }

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
