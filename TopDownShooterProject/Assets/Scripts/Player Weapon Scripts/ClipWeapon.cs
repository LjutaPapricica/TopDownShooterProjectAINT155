using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipWeapon : Weapon {

    public float clipReloadTime = 5f;
    public int clipSize = 10;

    public int currentClip;

    private bool isClipLoaded = true;


    // Use this for initialization
    protected void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAmmoSystem = transform.parent.GetComponent<AmmoSystem>();

        currentClip = clipSize;

    }

    protected override void Fire()
    {

        isFiring = true;
        currentClip--;

        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);
        }       

        myAudioSource.PlayOneShot(firingSound);

        Invoke("SetFiring", fireTime);
        onFire.Invoke();
    }
    

    protected override void Update()
    {
        CheckIfClipIsEmpty();

        FireControl();
    }

    private void CheckIfClipIsEmpty()
    {
        if (currentClip <= 0 && isClipLoaded == true)
        {
            isClipLoaded = false;
            if (myAmmoSystem.GetAmmoCount() >= shotAmmoValue)
            {
                Invoke("ReloadClip", clipReloadTime);
            }
        }
    }

    private void ReloadClip()
    {
        if (myAmmoSystem.GetAmmoCount() < (clipSize * shotAmmoValue))
        {
            currentClip = myAmmoSystem.GetAmmoCount() / shotAmmoValue;
            myAmmoSystem.DecreaseAmmoCount(currentClip * currentClip);
        }
        else
        {
            myAmmoSystem.DecreaseAmmoCount(shotAmmoValue * clipSize);
            currentClip = clipSize;
        }
        

        isClipLoaded = true;
        onFire.Invoke();
    }

    protected override void FireControl()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isFiring && isClipLoaded && (heatValue == 0 || myWeaponHeatSystem.GetWeaponHeat()))
            {
                Fire();
            }
        }
    }

    public override string GetShotsLeft()
    {
        
        int ammoCount = myAmmoSystem.GetAmmoCount();
        
        int shotsLeft = ammoCount / shotAmmoValue;

        if (currentClip == 0)
        {
            return "\n" + currentClip.ToString() + "/" + shotsLeft.ToString() + " Reloading...";
        }
        else
        {
            return "\n" + currentClip.ToString() + "/" + shotsLeft.ToString();
        }
    }

}
