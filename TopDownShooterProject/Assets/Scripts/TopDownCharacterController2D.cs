using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController2D : MonoBehaviour {

    [SerializeField] float speed = 5.0f;
    Rigidbody2D myRigidBody2D;

	// Use this for initialization
	void Start ()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        float forwardThrusterInput = Input.GetAxis("Horizontal");
        float sideThrusterInput = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(speed * forwardThrusterInput, speed * sideThrusterInput);

        myRigidBody2D.AddForce(newVelocity);

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
