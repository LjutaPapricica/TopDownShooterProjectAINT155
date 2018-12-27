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

    private Animator gunAnim;

    private void Start()
    {
        gunAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gunAnim != null)
        {
            if (Input.GetMouseButton(0))
            {
                gunAnim.SetBool("isFiring", true);
            }
            else
            {
                gunAnim.SetBool("isFiring", false);
            }
        }
    }

    public void SendHealthData(int health)
    {
        if (OnUpdateHealth != null)
        {
            OnUpdateHealth(health);
        }
    }

    public void SendAmmoData(int ammoCount)
    {
        if (OnUpdateAmmoCount != null)
        {
            OnUpdateAmmoCount(ammoCount);
        }
    }

    public void SendWeaponHeatData(int weaponHeat)
    {
        if (OnUpdateWeaponHeat != null)
        {
            OnUpdateWeaponHeat(weaponHeat);
        }
    }
}
