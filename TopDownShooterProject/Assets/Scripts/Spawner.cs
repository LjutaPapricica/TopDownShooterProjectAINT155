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
        Vector3 rotationinDegrees = transform.eulerAngles;
        rotationinDegrees.z += adjustmentAngle;

        Quaternion rotationInRadians = Quaternion.Euler(rotationinDegrees);

        if(parentTransform != null)
        {
            if(parentTransform.childCount < maxNumOfSpawns)
            {
                Instantiate(prefabToSpawn, transform.position, rotationInRadians, parentTransform);
            }            
        }
        else
        {
            Instantiate(prefabToSpawn, transform.position, rotationInRadians);
        }
        
    }
}
