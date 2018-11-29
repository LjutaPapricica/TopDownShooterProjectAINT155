using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Event OnFireEvent;
    public AudioClip firingSound;
    private AudioSource myAudioSource;
    public float fireTime = 0.5f;
    public int ammoCount = 50;

    private bool isFiring = false;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void SetFiring()
    {
        isFiring = false;
    }

    private void Fire()
    {
        isFiring = true;
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        ammoCount--;
        GetComponent<Player>().SendAmmoData(ammoCount);

        myAudioSource.PlayOneShot(firingSound);

        Invoke("SetFiring", fireTime);
    }
    
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            if (!isFiring && (ammoCount > 0))
            {
                Fire();
            }
        }
	}
}
