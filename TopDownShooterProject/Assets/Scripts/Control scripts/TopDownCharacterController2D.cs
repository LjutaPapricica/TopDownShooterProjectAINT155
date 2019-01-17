using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController2D : MonoBehaviour {

    public float speed = 5f;
    Rigidbody2D myRigidBody2D;

    public AudioClip movementSound;
    private AudioSource myAudioSource;
    private bool isSoundPlaying = false;

	// Use this for initialization
	void Start ()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = movementSound;
    }

    private void FixedUpdate()
    {
        //gets magnintude of horizontal force when pressing keys 'a' or 'd' returning float value between -1 and 1
        float forwardThrusterInput = Input.GetAxisRaw("Horizontal");
        //gets magnintude of vertical force when pressing keys 'w' or 's' returning a float value between -1 and 1
        float sideThrusterInput = Input.GetAxisRaw("Vertical");
        
        //new velocity of player tank calculated depending on magnitude of vertical and horizontal force
        Vector2 newVelocity = new Vector2(speed * forwardThrusterInput, speed * sideThrusterInput);

        //new force added to rigidbody2d
        myRigidBody2D.AddForce(newVelocity);       
               
        //if a value greater or less than 0 is returned for the magnitude of horizontal or verticle force
        //and no movement sound is playing, playing moving sounds 
        if ((forwardThrusterInput != 0 || sideThrusterInput != 0) && !isSoundPlaying)
        {
            myAudioSource.Play();
            isSoundPlaying = true;
        }
        //if there is no acceleration in either direction and no moving sound is playing
        //then stop playing moving sounds
        else if ((forwardThrusterInput == 0 && sideThrusterInput == 0) && isSoundPlaying)
        {
            myAudioSource.Stop();
            isSoundPlaying = false;
        }

        //OLD CODE USED FOR PREVIOUS MOVEMENT CONTROLS
        //if (forwardThrusterInput != 0)
        //{
        //    myRigidBody2D.AddForce(transform.right * speed * forwardThrusterInput);
        //}
        //if (sideThrusterInput != 0)
        //{
        //    myRigidBody2D.AddForce(transform.up * speed * sideThrusterInput);
        //}

        //no rotation velocity since rotation movement is done directly through the transform
        myRigidBody2D.angularVelocity = 0.0f;
    }
    
}
