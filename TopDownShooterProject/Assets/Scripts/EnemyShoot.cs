using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public Transform turret;
    public float maxDistance = 100f;
    float originOffSet = 0.5f;

    //public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity);

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D hit = Physics2D.Raycast(turret.position, turret.up, Mathf.Infinity);
        Debug.DrawRay(turret.position, turret.up, Color.red);

        if (hit.collider != null)
        {
            Debug.Log("I hit" + hit.collider.name);
        }
	}
}
