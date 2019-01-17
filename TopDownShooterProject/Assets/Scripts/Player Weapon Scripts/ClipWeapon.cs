using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clip weapon inherits behaviours and properties from the weapon class script
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

        //at the start the clip is fully loaded
        currentClip = clipSize;
    }

    protected override void Fire()
    {
        isFiring = true;

        //when firing each shot the current clip is decemented by 1
        currentClip--;

        //can spawn bullets from multiple hardpoints on the tank by getting positions from transform array
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);
        }       

        //Plays firing sound without being interrupted
        myAudioSource.PlayOneShot(firingSound);

        //weapon can be fired again when reloaded after a set time period
        Invoke("SetFiring", fireTime);
        //any listeners on the event of fire are called
        onFire.Invoke();
    }
    

    protected override void Update()
    {
        //checks if the clip is empty
        CheckIfClipIsEmpty();

        FireControl();
    }

    private void CheckIfClipIsEmpty()
    {
        //if the clip reaches zero and the clip is known to be loaded then set the clip to not loaded
        if (currentClip <= 0 && isClipLoaded == true)
        {
            isClipLoaded = false;

            //if the player has ammo greater than the ammo value of one shot in the clip then reload weapon after set time period
            if (myAmmoSystem.GetAmmoCount() >= shotAmmoValue)
            {
                Invoke("ReloadClip", clipReloadTime);
            }
        }
    }

    private void ReloadClip()
    {
        //if the player has less ammo than what is needed for a full clip then partially fill the clip 
        //with however much ammo the player has left
        if (myAmmoSystem.GetAmmoCount() < (clipSize * shotAmmoValue))
        {
            currentClip = myAmmoSystem.GetAmmoCount() / shotAmmoValue;
            myAmmoSystem.DecreaseAmmoCount(currentClip * currentClip);
        }
        //otherwise fill the entire clip
        else
        {
            myAmmoSystem.DecreaseAmmoCount(shotAmmoValue * clipSize);
            currentClip = clipSize;
        }
        
        //clip set to loaded and listeners of event onFire are called
        isClipLoaded = true;
        onFire.Invoke();
    }

    protected override void FireControl()
    {
        //when left mouse button is clicked 
        if (Input.GetMouseButton(0))
        {
            //if the weapon is not reloading, the clip is loaded and heat is not an issue then fire the weapon
            if (!isFiring && isClipLoaded && (heatValue == 0 || myWeaponHeatSystem.GetWeaponHeat()))
            {
                Fire();
            }
        }
    }

    //Prints the number of shots left that the clip can fire and the total shots that can be fired on the screen
    public override string GetShotsLeft()
    {
        //gets ammount of ammo the player has
        int ammoCount = myAmmoSystem.GetAmmoCount();
        
        //number of shots left is calculated by the ammo count divided by the shot ammo value
        int shotsLeft = ammoCount / shotAmmoValue;

        //if the clip is empty but the player has ammo then "Reloading" text is shown to indicate to the player
        //that the weapon clip is being reloaded
        if (currentClip == 0 && ammoCount > 0)
        {
            return "\n" + currentClip.ToString() + "/" + shotsLeft.ToString() + " Reloading...";
        }
        //else if the weapon clip has ammo or the player has completely run out of ammo then do not display "Reloading" to the player
        else
        {
            return "\n" + currentClip.ToString() + "/" + shotsLeft.ToString();
        }
    }

}
