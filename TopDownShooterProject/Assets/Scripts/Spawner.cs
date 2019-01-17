using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject prefabToSpawn;
    public float adjustmentAngle = 0f;
    public int maxNumOfSpawns = 10;

    public Transform parentTransform;

    public void Spawn()
    {
        //This code can be used to re adjust rotation of spawned gameobject
        //Vector3 rotationinDegrees = transform.eulerAngles;
        //rotationinDegrees.z += adjustmentAngle;

        //Quaternion rotationInRadians = Quaternion.Euler(rotationinDegrees);

        //if the parent transform is there
        if(parentTransform != null)
        {
            //if the child count of the parent is less then the maximum number of spawns then spawn the gameobject
            if(parentTransform.childCount < maxNumOfSpawns)
            {
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity, parentTransform);
            }            
        }
        //if there is no parent transform then the gameobject is spawned without a maximum limit
        else
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
        
    }
}
