using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


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
    

    private void Start()
    {
        //gets pathfinding components
        ai = GetComponent<IAstarAI>();
        aiLerp =  GetComponent<AILerp>();

        rb = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();

        //checks whether gameobject can move in pathfinder
        isMoving = aiLerp.canMove;

        //stationary timeout is the amount of time the tank does not see the target before its starts to move again
        stationaryTimeOut = Random.Range(1f, 5f);
    }

    private void Update()
    {
        //velocity should always be 0 because the tank moves by changing its transform position
        rb.velocity = new Vector2(0f, 0f);
        
        MoveAndShoot();
        PlayMovingSound();

    }

    //this method is called when the tank casts a ray which hits its target meaning the target is within sight
    public void CanSeeTarget()
    {
        seeTarget = true;
    }

    //this method is called when the raycast hits an obstacle or the target is out of range meaning the target cannot be seen
    public void CannotSeeTarget()
    {
        seeTarget = false;
    }
    
    private void PlayMovingSound()
    {
        //when the tank is moving and there is no sound being played, play the movement sound
        if (isMoving && !isPlayingSound)
        {
            isPlayingSound = true;
            myAudioSource.clip = movementSound[Random.Range(0, movementSound.Length - 1)];
            myAudioSource.Play();
        }
        //when the tank is not moving and the movement sound is playing, stop playing the movement sound
        else if (!isMoving && isPlayingSound)
        {
            myAudioSource.Stop();
            isPlayingSound = false;
        }
    }

    private void MoveAndShoot()
    {
        //if the tank cannot see the target and is not moving the timer increases
        if (!seeTarget && !isMoving)
        {
            time += Time.deltaTime;
        }

        //if theere is a target assinged and the ai component is there
        if (target != null && ai != null)
        {
            //the destination of the pathfinder is set to be the transform of the target
            ai.destination = target.position;
            //caluclates shortest path to the target transform
            ai.SearchPath();

            //distance between target and this gameobject calculated
            float dist = Vector3.Distance(transform.position, target.position);

            //if the distance is smaller than the stopping distance (distance to target before stopping)
            //and the tank is moving and the target can be seen, the tank stops
            if (dist < stoppingDistance && isMoving && seeTarget) //STOP
            {
                time = 0f;
                isMoving = false;

                aiLerp.canMove = false;
            }
            //if the distance to the target is greater than the moving distance (distance to target exceeds threshold)
            //and the time is greater than the stationary timeout then the tank starts moving 
            else if (dist > movingDistance || (time > stationaryTimeOut)) //MOVE
            {
                isMoving = true;
                aiLerp.canMove = true;

            }


        }
    }
    //new target can be set
    public void SetTarget(Transform newTarget)
    {
         target = newTarget;
    }
}
