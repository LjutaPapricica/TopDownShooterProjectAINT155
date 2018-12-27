using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpWeapon : Weapon {
    
    private float sizeMultiplier = 1f;
    public float damageMultiplier = 1f;

    private bool isCharged = false;

    protected override void FireControl()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isFiring && (myWeaponHeatSystem.GetWeaponHeat()))
            {
                BuildUpShot();

                if (!isCharged)
                {
                    isCharged = true;
                    myWeaponHeatSystem.IncreaseWeaponHeat(10);
                }
            }
        }

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

            projectile.transform.localScale *= sizeMultiplier;

            float damage = projectile.GetComponent<Bullet>().damage;
            damage *= damageMultiplier;

            projectile.GetComponent<Bullet>().damage = Convert.ToInt32(damage);
        }

        damageMultiplier = 1f;
        sizeMultiplier = 1f;

        myAmmoSystem.DecreaseAmmoCount(shotAmmoValue);

        myAudioSource.PlayOneShot(firingSound);

        Invoke("SetFiring", fireTime);
    }
}
