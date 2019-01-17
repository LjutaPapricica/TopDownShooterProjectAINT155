using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAllyWeaponControl : MonoBehaviour {
    
    //point of vision is where the ray is casted
    public Transform pointOfVision;
    public float maxRange = 5f;

    public UnityEvent onClearToFire;
    public UnityEvent onNotClearToFire;
	
	// Update is called once per frame
	void Update()
    {
        //raycasts will only hit objects such as enemy tanks, enemy turrets, buildings and obstacles included in the layer mask
        //raycasts will ignore the player and ally tanks because there is no team damage
        int layerMaskObstacle = 1 << 0;
        int layerMaskEnemy = 1 << 9;
        int layerMaskEnemyStationary = 1 << 15;

        //Fianl layer mask determines which layers the raycast ignores
        int finalLayerMask = layerMaskObstacle | layerMaskEnemy | layerMaskEnemyStationary;

        //ray is casted from point of vision in the direction the turret is facing
        //max range limits how far the ray can travel so the tank must travel closer to its target
        RaycastHit2D hit = Physics2D.Raycast(pointOfVision.position, pointOfVision.up, maxRange, finalLayerMask);
        //visual line to show ray for debugging purposes
        Debug.DrawRay(pointOfVision.position, pointOfVision.up, Color.red);
        
        //if the ray does hit something within the the layermask
        if (hit.collider != null)
        {
            //if the ray hits a gameobject with the tag "enemy" then target is within sight
            if (hit.collider.tag == "Enemy")
            {
                //tank can become stationary if close enough to target in the pathfinder script
                //tells weapons that they can start firing
                onClearToFire.Invoke();
            }
            //else if the ray hits anything else it has no clear line of sight on an enemy
            else
            {
                //resets stationary timeout in pathfinder script
                //tells weapons to stop firing
                onNotClearToFire.Invoke();
            }
        }
    }
    
}
