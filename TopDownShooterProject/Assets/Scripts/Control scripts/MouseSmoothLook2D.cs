using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSmoothLook2D : MonoBehaviour {
    [SerializeField] Camera theCamera;
    [SerializeField] float smoothing = 5.0f;
    [SerializeField] float adjustmentAngle = 0.0f;

	
	void Update ()
    {
        //gets position of mouse cursor on the screen
        Vector3 target = theCamera.ScreenToWorldPoint(Input.mousePosition);
        //vector difference calculated by taking away position of mouse cursor from position of player tank
        Vector3 difference = target - transform.position;

        //difference is normalized s the calculated vector now has an overall magnitude of 1
        difference.Normalize();

        //amount of rotation on the z axis between position of mouse cursor and the player tank calculated 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //adjustment angle added if player tank sprite is facing wrong direction
        Quaternion newRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotZ + adjustmentAngle));
        //smooth rotation of player tank to point towards position of mouse cursor
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * smoothing);
	}
}
