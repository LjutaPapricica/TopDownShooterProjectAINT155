using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour {
    
    [SerializeField] int maxAmmoCapacity = 1000;
    private int ammoCount;
    private Player thePlayer;

    private void Start()
    {
        thePlayer = transform.parent.GetComponent<Player>();
        ammoCount = maxAmmoCapacity;
    }

    public void DecreaseAmmoCount(int shotAmmoValue)
    {
        ammoCount = Mathf.Clamp(ammoCount - shotAmmoValue, 0, maxAmmoCapacity);

        thePlayer.SendAmmoData(ammoCount);
    }

    public void IncreaseAmmoCount(int ammoResupply)
    {
        ammoCount = Mathf.Clamp(ammoCount + ammoResupply, 0, maxAmmoCapacity);

        thePlayer.SendAmmoData(ammoCount);
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }
}
