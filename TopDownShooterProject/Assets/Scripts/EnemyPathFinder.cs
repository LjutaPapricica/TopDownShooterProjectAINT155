using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFinder : MonoBehaviour {

    public Transform target;
    private IAstarAI ai;
    private AILerp aiLerp;
    private Rigidbody2D rb;
    private EnemyShoot es;
    public bool seePlayer, isMoving;
    private float time = 0f, stationaryTimeOut;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        aiLerp =  GetComponent<AILerp>();
        rb = GetComponent<Rigidbody2D>();
        es = GetComponent<EnemyShoot>();

        isMoving = aiLerp.canMove;
        stationaryTimeOut = Random.Range(1f, 5f);
    }

    private void Update()
    {
        rb.velocity = new Vector2(0f, 0f);
        seePlayer = es.GetSeePlayer();

        if (!seePlayer && !isMoving)
        {
            time += Time.deltaTime;
        }

        if (target != null && ai != null)
        {
            ai.destination = target.position;
            ai.SearchPath();

            float dist = Vector3.Distance(transform.position, target.position);            
            
            if (dist < 3 && isMoving && seePlayer) //STOP
            {
                time = 0f;
                isMoving = false;

                aiLerp.canMove = false;
            }
            else if (dist > 6 || (time > stationaryTimeOut)) //MOVE
            {
                isMoving = true;
                aiLerp.canMove = true;
                
            }

        }

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
