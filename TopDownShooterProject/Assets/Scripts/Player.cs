using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public delegate void UpdateHealth(int newHealth);
    public static event UpdateHealth OnUpdateHealth;

    public delegate void UpdateAmmoCount(int newAmmoCount);
    public static event UpdateAmmoCount OnUpdateAmmoCount;

    public delegate void UpdateWeaponHeat(int newWeaponHeat);
    public static event UpdateWeaponHeat OnUpdateWeaponHeat;
    
    //send total health value to health bar UI
    public void SendHealthData(int health)
    {
        if (OnUpdateHealth != null)
        {
            OnUpdateHealth(health);
        }
    }

    //send total ammo value to ammo bar UI
    public void SendAmmoData(int ammoCount)
    {
        if (OnUpdateAmmoCount != null)
        {
            OnUpdateAmmoCount(ammoCount);
        }
    }

    //send total heat value to heat bar UI
    public void SendWeaponHeatData(int weaponHeat)
    {
        if (OnUpdateWeaponHeat != null)
        {
            OnUpdateWeaponHeat(weaponHeat);
        }
    }
}
