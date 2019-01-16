using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIEnemyWeaponControl : MonoBehaviour {

    public Transform pointOfVision;
    public float maxRange = 5f;

    public UnityEvent onClearToFire;
    public UnityEvent onNotClearToFire;

    // Update is called once per frame
    void Update()
    {
        int layerMaskPlayer = 1 << 10;
        int layerMaskObstacle = 1 << 0;
        int layerMaskAlly = 1 << 14;

        int finalLayerMask = layerMaskObstacle | layerMaskPlayer | layerMaskAlly;

        RaycastHit2D hit = Physics2D.Raycast(pointOfVision.position, pointOfVision.up, maxRange, finalLayerMask);
        Debug.DrawRay(pointOfVision.position, pointOfVision.up, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                onClearToFire.Invoke();
            }
            else
            {
                onNotClearToFire.Invoke();
            }
        }
    }
}
