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
        GetComponent<Rigidbody2D>().AddForce(transform.up * moveSpeed);		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        Die();
    }

    private void OnBecameInvisible()
    {
        Die();
    }

    // Update is called once per frame
    private void Die()
    {
        OnDie.Invoke();
    }
}
