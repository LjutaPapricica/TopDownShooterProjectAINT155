using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum UnitType
{
    Enemy,
    Ally
}

public class UnitPathFinder : MonoBehaviour {
    
    private IAstarAI ai;
    private AILerp aiLerp;
    private Rigidbody2D rb;

    private AudioSource myAudioSource;
    public AudioClip[] movementSound;

    public int stoppingDistance = 3;
    public int movingDistance = 6;

    private bool seeTarget, isMoving, isPlayingSound;
    private float time = 0f, stationaryTimeOut;
    
    private Transform target;

    private UnitType unitType;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        aiLerp =  GetComponent<AILerp>();
        rb = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();

        isMoving = aiLerp.canMove;
        stationaryTimeOut = Random.Range(1f, 5f);
    }

    private void Update()
    {
        rb.velocity = new Vector2(0f, 0f);
        
        MoveAndShoot();
        PlayMovingSound();

    }

    public void CanSeeTarget()
    {
        seeTarget = true;
    }

    public void CannotSeeTarget()
    {
        seeTarget = false;
    }

    private void PlayMovingSound()
    {
        if (isMoving && !isPlayingSound)
        {
            isPlayingSound = true;
            myAudioSource.clip = movementSound[Random.Range(0, movementSound.Length - 1)];
            myAudioSource.Play();
        }
        else if (!isMoving && isPlayingSound)
        {
            myAudioSource.Stop();
            isPlayingSound = false;
        }
    }

    private void MoveAndShoot()
    {

        if (!seeTarget && !isMoving)
        {
            time += Time.deltaTime;
        }

        if (target != null && ai != null)
        {
            ai.destination = target.position;
            ai.SearchPath();

            float dist = Vector3.Distance(transform.position, target.position);

            if (dist < stoppingDistance && isMoving && seeTarget) //STOP
            {
                time = 0f;
                isMoving = false;

                aiLerp.canMove = false;
            }
            else if (dist > movingDistance || (time > stationaryTimeOut)) //MOVE
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
