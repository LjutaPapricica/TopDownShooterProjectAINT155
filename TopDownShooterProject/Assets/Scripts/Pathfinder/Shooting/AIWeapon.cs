using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapon : MonoBehaviour {

    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float reloadTime = 2f;
    private bool isReloaded = true;

    private bool seeTarget;
    private AudioSource theAudioSource;
    public AudioClip fireSound;

	// Use this for initialization
	void Start ()
    {
        theAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        FireControl();
	}

    public void CanSeeTarget()
    {
        seeTarget = true;
    }

    public void CannotSeeTarget()
    {
        seeTarget = false;
    }

    private void FireControl()
    {
        if (isReloaded && seeTarget)
        {
            Fire();
        }
    }

    private void Fire()
    {
            isReloaded = false; 
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            theAudioSource.PlayOneShot(fireSound);

            Invoke("SetReloaded", reloadTime);            
    }

    private void SetReloaded()
    {
        isReloaded = true;
    }
}
