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
    
    public UnityEvent onFire;

    protected WeaponHeatSystem myWeaponHeatSystem;
    protected AmmoSystem myAmmoSystem;

    public float fireTime = 0.5f;

    public int shotAmmoValue = 1;
    public int heatValue = 1;

    protected bool isFiring = false;

    private void Awake()
    {
        //gets ammo system and weapon heat system from the parent transform weapon manager
        myAmmoSystem = transform.parent.GetComponent<AmmoSystem>();
        myAudioSource = GetComponent<AudioSource>();
        myWeaponHeatSystem = transform.parent.GetComponent<WeaponHeatSystem>();
    }

    //called when the weapon has reloaded
    protected void SetFiring()
    {
        isFiring = false;
    }

    protected virtual void Fire()
    {
        //When weapon is fired all listeners attached to the onFire event are called
        onFire.Invoke();

        //weapon cannot fire until it has been reloaded
        isFiring = true;

        //projectiles can be instantiated from more that one hardpoint on the player tank
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);
        }

        //if the projectile requires ammo then decrease the ammo count
        if (shotAmmoValue > 0)
        myAmmoSystem.DecreaseAmmoCount(shotAmmoValue);

        //if the weapon produces heat then increase total heat
        if (heatValue > 0)
        myWeaponHeatSystem.IncreaseWeaponHeat(heatValue);

        //play shot sound effect
        myAudioSource.PlayOneShot(firingSound);

        //Reload the weapon after a set time period
        Invoke("SetFiring", fireTime);
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        FireControl();
    }

    protected virtual void FireControl()
    {
        //when the left mouse button is clicked
        if (Input.GetMouseButton(0))
        {
            //if the weapon is reloaded and the weapon has / does not need ammo and the heat threshold has not been reached / does not produce heat
            //then the weapon can be fired
            if (!isFiring && (shotAmmoValue == 0 || myAmmoSystem.GetAmmoCount() > 0) && (heatValue == 0 || myWeaponHeatSystem.GetWeaponHeat()))
            {
                Fire();
            }
        }
    }


    public virtual string GetShotsLeft()
    {
        //if the weapon requires ammunition then display the number of shots remaining
        if (shotAmmoValue > 0)
        {
            //gets ammo count
            int ammoCount = myAmmoSystem.GetAmmoCount();

            //number of shots left calculated by dividing the ammo count by the ammo value of each shot
            int shotsLeft = ammoCount / shotAmmoValue;

            //returns a string to the weapon UI to display to the user
            return "\n" + shotsLeft.ToString();
        }
        else return null;
    }
}
