using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform[] bulletSpawn;
    public Event OnFireEvent;
    public AudioClip firingSound;
    private AudioSource myAudioSource;
    private WeaponManager myWeaponManager;
    public float fireTime = 0.5f;
    public int shotAmmoValue = 1;

    private bool isFiring = false;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myWeaponManager = transform.parent.GetComponent<WeaponManager>();        
    }

    private void SetFiring()
    {
        isFiring = false;
    }

    private void Fire()
    {
        isFiring = true;

        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);
        }

        myWeaponManager.DecreaseAmmoCount(shotAmmoValue);

        myAudioSource.PlayOneShot(firingSound);

        Invoke("SetFiring", fireTime);
    }
    
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            if (!isFiring && (myWeaponManager.GetAmmoCount() > 0))
            {
                Fire();
            }
        }
	}
}
