using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnUpdateTarget : UnityEvent<Transform> { }
public class UpdateTarget : MonoBehaviour {

    public OnUpdateTarget onUpdateTarget;

    public GameObject closestAdversary;
    private GameObject[] adversaries;

    public float refreshTime = 5f;

    private void Start()
    {
        //as soon as unit is spawned in the game it searches for the calculates its nearest target
        //from then on the closest target is recalculated at a set time interval
        InvokeRepeating("FindClosestAdversary", 0f, refreshTime);
    }

    private void Update()
    {
        //if the target has been killed it will appear as null
        //therefore the unit will instantly recalculate the new closest target without having to wait for the refresh
        if (closestAdversary == null)
        {
            FindClosestAdversary();
        }
    }

    private void FindClosestAdversary()
    {
        //if the unit is an enemy it will search for all gameobjects with the tag "Player" and stores them in a gameobject array
        //this includes searching for ally tanks anf ally factories
        if (tag == "Enemy")
        {
            adversaries = GameObject.FindGameObjectsWithTag("Player");
        }
        //if the unit is an ally tank it will search for all gameobjects with the tag "Enemy" and stores them in a gameobject array
        else if (tag == "Player")
        {
            adversaries = GameObject.FindGameObjectsWithTag("Enemy");
        }

        //if there are no targets to be found then set closest adversary to null
        //this tells the unit to stop moving in the smoothLookAtTarget2D and pathfinder scripts
        if (adversaries == null)
        {
            closestAdversary = null;
            onUpdateTarget.Invoke(closestAdversary.transform);
        }

        //shortest distance initially set to infinity
        float shortestDist = Mathf.Infinity;

        //goes through each gameobject in the gameobject array (cycles through list of avaiable targets)
        foreach (GameObject adversary in adversaries)
        {
            //distance calculated between this unit and the current target in the adversaries array
            float dist = Vector3.Distance(transform.position, adversary.transform.position);

            //if the distance caluclated is shorter than the shortest distance
            //set this target to to the new closest target and set this distance as the new shortest distance
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closestAdversary = adversary.gameObject;
            }
            
            //if there is a closest target then send its target transform to listners by invoking the event
            if (closestAdversary != null)
            {
                onUpdateTarget.Invoke(closestAdversary.transform);
            }
        }

    }
}
