using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Weapon : MonoBehaviour {
    
    public GameObject bulletPrefab;
    public Transform[] bulletSpawn;
    public Event OnFireEvent;
    public AudioClip firingSound;
    protected AudioSource myAudioSource;

    protected WeaponManager myWeaponManager;
    protected WeaponHeatSystem myWeaponHeatSystem;
    protected AmmoSystem myAmmoSystem;

    public float fireTime = 0.5f;

    public int shotAmmoValue = 1;
    public int heatValue = 1;

    protected bool isFiring = false;

    protected void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myWeaponManager = transform.parent.GetComponent<WeaponManager>();       
        myWeaponHeatSystem = transform.parent.GetComponent<WeaponHeatSystem>();
        myAmmoSystem = transform.parent.GetComponent<AmmoSystem>();
    }

    protected void SetFiring()
    {
        isFiring = false;
    }

    protected virtual void Fire()
    {
        isFiring = true;

        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);
        }

        myAmmoSystem.DecreaseAmmoCount(shotAmmoValue);

        myWeaponHeatSystem.IncreaseWeaponHeat(heatValue);

        myAudioSource.PlayOneShot(firingSound);

        Invoke("SetFiring", fireTime);
    }

    // Update is called once per frame
    protected void Update ()
    {
        FireControl();
    }

    protected virtual void FireControl()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isFiring && (myAmmoSystem.GetAmmoCount() > 0) && (myWeaponHeatSystem.GetWeaponHeat()))
            {
                Fire();
            }
        }
    }
}
