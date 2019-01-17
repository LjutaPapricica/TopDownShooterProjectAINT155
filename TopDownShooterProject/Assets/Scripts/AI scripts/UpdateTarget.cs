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
        InvokeRepeating("FindClosestAdversary", 0f, refreshTime);
    }

    private void Update()
    {
        if (closestAdversary == null)
        {
            FindClosestAdversary();
        }
    }

    private void FindClosestAdversary()
    {
        if (tag == "Enemy")
        {
            adversaries = GameObject.FindGameObjectsWithTag("Player");
        }
        else if (tag == "Player")
        {
            adversaries = GameObject.FindGameObjectsWithTag("Enemy");
        }

        if (adversaries == null)
        {
            closestAdversary = null;
            return;
        }

        float shortestDist = Mathf.Infinity;

        foreach (GameObject adversary in adversaries)
        {
            float dist = Vector3.Distance(transform.position, adversary.transform.position);

            if (dist < shortestDist)
            {
                shortestDist = dist;
                closestAdversary = adversary.gameObject;
            }
            
            if (closestAdversary != null)
            {
                onUpdateTarget.Invoke(closestAdversary.transform);
            }
        }

    }
}
