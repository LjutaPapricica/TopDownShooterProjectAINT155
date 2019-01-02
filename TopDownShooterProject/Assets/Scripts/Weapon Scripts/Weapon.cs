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
        myAmmoSystem = transform.parent.GetComponent<AmmoSystem>();
        myAudioSource = GetComponent<AudioSource>();
        myWeaponHeatSystem = transform.parent.GetComponent<WeaponHeatSystem>();
    }

    protected void SetFiring()
    {
        isFiring = false;
    }

    protected virtual void Fire()
    {
        onFire.Invoke();

        isFiring = true;

        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);
        }

        if (shotAmmoValue > 0)
        myAmmoSystem.DecreaseAmmoCount(shotAmmoValue);

        if (heatValue > 0)
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
            if (!isFiring && (shotAmmoValue == 0 || myAmmoSystem.GetAmmoCount() > 0) && (heatValue == 0 || myWeaponHeatSystem.GetWeaponHeat()))
            {
                Fire();
            }
        }
    }


    public virtual string GetShotsLeft()
    {
        if (shotAmmoValue > 0)
        {
            int ammoCount = myAmmoSystem.GetAmmoCount();
            int shotsLeft = ammoCount / shotAmmoValue;

            return "\n" + shotsLeft.ToString();
        }
        else return null;
    }
}
