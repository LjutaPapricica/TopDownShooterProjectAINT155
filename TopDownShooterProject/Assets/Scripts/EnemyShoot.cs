using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public Transform gunEnd;
    public float maxRange = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float reloadTime = 0.5f;


    private bool isFiring = false;

    private void SetNotFiring()
    {
        isFiring = false;
    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D hit = Physics2D.Raycast(gunEnd.position, gunEnd.up, maxRange);
        Debug.DrawRay(gunEnd.position, gunEnd.up, Color.red);

        if (hit.collider != null) 
        {
            if (hit.collider.tag == "Player" && !isFiring)
            {
                Fire();
                Debug.Log("I hit" + hit.collider.name);
            }
        }
	}

    private void Fire()
    {
        isFiring = true;
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }

        Invoke("SetNotFiring", reloadTime);
    }
}
