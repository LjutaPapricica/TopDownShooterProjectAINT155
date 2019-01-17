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

    //triggered by AI weapon control script when target is within range and in sight
    public void CanSeeTarget()
    {
        seeTarget = true;
    }

    //triggered by AI weapon control script when target is not within sight or out of range
    public void CannotSeeTarget()
    {
        seeTarget = false;
    }

    private void FireControl()
    {
        //check if weapon reloaded and target in sight
        if (isReloaded && seeTarget)
        {
            Fire();
        }
    }

    private void Fire()
    {
            //if weapon is loaded spawn projectile at gun end and fire it in direction towards target
            isReloaded = false; 
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            theAudioSource.PlayOneShot(fireSound);

            //gun can fire once gun is reloaded after set time period
            Invoke("SetReloaded", reloadTime);            
    }

    private void SetReloaded()
    {
        //weapon can now fire again
        isReloaded = true;
    }
}
