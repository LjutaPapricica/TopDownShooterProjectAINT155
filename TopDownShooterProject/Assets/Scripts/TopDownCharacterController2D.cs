using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController2D : MonoBehaviour {

    public float speed = 5.0f;
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
        float forwardThrusterInput = Input.GetAxis("Horizontal");
        float sideThrusterInput = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(speed * forwardThrusterInput, speed * sideThrusterInput);

        myRigidBody2D.AddForce(newVelocity);

        if ((forwardThrusterInput != 0 || sideThrusterInput != 0) && !isSoundPlaying)
        {
             myAudioSource.Play();
             isSoundPlaying = true;            
        }
        else if ((forwardThrusterInput == 0 && sideThrusterInput == 0) && isSoundPlaying)
        {
            myAudioSource.Stop();
            isSoundPlaying = false;
        }

            //if (forwardThrusterInput != 0)
            //{
            //    myRigidBody2D.AddForce(transform.right * speed * forwardThrusterInput);
            //}
            //if (sideThrusterInput != 0)
            //{
            //    myRigidBody2D.AddForce(transform.up * speed * sideThrusterInput);
            //}

            myRigidBody2D.angularVelocity = 0.0f;
    }
}
