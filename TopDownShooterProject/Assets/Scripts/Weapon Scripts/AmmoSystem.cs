using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour {
    
    [SerializeField] int ammoCount = 1000;

    private Player thePlayer;

    private void Start()
    {
        thePlayer = transform.parent.GetComponent<Player>();
    }

    public void DecreaseAmmoCount(int shotAmmoValue)
    {
        ammoCount -= shotAmmoValue;

        thePlayer.SendAmmoData(ammoCount);
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }
}
