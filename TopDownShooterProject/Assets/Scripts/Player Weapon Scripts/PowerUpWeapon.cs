using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//powerup weapon inherits behaviours and properties from the weapon class script
public class PowerUpWeapon : Weapon {
    
    private float sizeMultiplier = 1f;
    public float damageMultiplier = 1f;
    public int initialWeaponHeatIncrease = 10;

    private bool isCharged = false;

    protected override void FireControl()
    {
        //if the user presses the left mouse button
        if (Input.GetMouseButton(0))
        {
            //if the weapon is reloaded and the weapon heat does not exceed the threshold then
            //fire the weapon
            if (!isFiring && (myWeaponHeatSystem.GetWeaponHeat()))
            {
                //builds up shot every update call as the mouse button is held down
                BuildUpShot();

                //the weapon is now beginning to charge so weapon heat is increased by a fixed amount initially
                //this acts as a small penalty to encourage the player to build bigger shots which have a higher
                //damage to heat ratio
                if (!isCharged)
                {
                    isCharged = true;
                    myWeaponHeatSystem.IncreaseWeaponHeat(initialWeaponHeatIncrease);
                }
            }
        }

        //when the left mouse button is released the weapon is fired and is therefore no longer charged
        if (Input.GetMouseButtonUp(0))
        {
            if (isCharged)
            {
                isCharged = false;
                Fire();
            }
            
        }
    }

    private void BuildUpShot()
    {
        //the damage, size of the projectile and accumulated heat is increased slowly over time
        damageMultiplier += 4 * Time.deltaTime;
        sizeMultiplier += 2 * Time.deltaTime;
        myWeaponHeatSystem.IncreaseWeaponHeat(1);
    }

    protected override void Fire()
    {
        isFiring = true;

        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            GameObject projectile = Instantiate(bulletPrefab, bulletSpawn[i].position, bulletSpawn[i].rotation);

            //when the projectile is fired its scale is multiplied by the scale multiplier 
            //the size of the projectile depends on how long the projectile was charged for
            projectile.transform.localScale *= sizeMultiplier;

            //the instantiated bullet damage property is increased dependng on how long the weapon was charged for
            float damage = projectile.GetComponent<Bullet>().damage;
            damage *= damageMultiplier;

            //damage must be rounded to an integer as the hitpoints of all units are whole numbers
            projectile.GetComponent<Bullet>().damage = Convert.ToInt32(damage);
        }

        //damage multiplier and size multiplier set back to 1 for the next shot
        damageMultiplier = 1f;
        sizeMultiplier = 1f;

        myAudioSource.PlayOneShot(firingSound);

        Invoke("SetFiring", fireTime);
    }
}
