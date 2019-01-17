using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 100.0f;
    public int damage = 1;

    public UnityEvent OnDie;

    // Use this for initialization
    void Start () {
        //when bullet created a force is immediately applied
        GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed);		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //object that collided with bullet takes damage
        //if other damage has no "TakeDamage" method then nothing happens
        other.transform.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        Die();
    }

    //bullet destroyed if cannot be seen on the camera
    private void OnBecameInvisible()
    {
        Die();
    }
    
    //listeners tied to OnDie Event triggered
    private void Die()
    {
        OnDie.Invoke();
    }
}
