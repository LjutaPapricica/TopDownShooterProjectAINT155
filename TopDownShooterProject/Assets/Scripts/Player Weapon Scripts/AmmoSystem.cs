using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoSystem : MonoBehaviour {

    public UnityEvent onAmmoChange;

    [SerializeField] int maxAmmoCapacity = 1000;
    private int ammoCount;
    private Player thePlayer;

    private void Start()
    {
        //the player script is on the player gameobject which is the parent of the weapon manager which the
        //ammo system is currently assigned to
        thePlayer = transform.parent.GetComponent<Player>();
        //at the start of the game the ammo count is set at the maximum value
        ammoCount = maxAmmoCapacity;
    }

    //called by weapons that use ammunition to decrease available ammuntion
    //shot ammo value depends on the projectile as some use more ammo than others
    public void DecreaseAmmoCount(int shotAmmoValue)
    {
        //ammo count is always kept above 0
        ammoCount = Mathf.Clamp(ammoCount - shotAmmoValue, 0, maxAmmoCapacity);

        //ammo bar UI is updated
        thePlayer.SendAmmoData(ammoCount);

        onAmmoChange.Invoke();
    }

    //called when player picks up ammo crates or sits on repair pad
    public void IncreaseAmmoCount(int ammoResupply)
    {
        //ammo count cannot exceed maximum capacity
        ammoCount = Mathf.Clamp(ammoCount + ammoResupply, 0, maxAmmoCapacity);

        //ammo bar UI is updated
        thePlayer.SendAmmoData(ammoCount);

        onAmmoChange.Invoke();
    }

    //weapons that require ammo fetch ammo count to calculate number of shots that can be fired
    //weapons do not shoot if not enough ammo
    public int GetAmmoCount()
    {
        return ammoCount;
    }

    //returns the maximum ammo capacity the player has
    public int GetMaxAmmoCount()
    {
        return maxAmmoCapacity;
    }
}
