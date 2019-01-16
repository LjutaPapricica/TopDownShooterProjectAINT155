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
        thePlayer = transform.parent.GetComponent<Player>();
        ammoCount = maxAmmoCapacity;
    }

    public void DecreaseAmmoCount(int shotAmmoValue)
    {
        ammoCount = Mathf.Clamp(ammoCount - shotAmmoValue, 0, maxAmmoCapacity);

        thePlayer.SendAmmoData(ammoCount);

        onAmmoChange.Invoke();
    }

    public void IncreaseAmmoCount(int ammoResupply)
    {
        ammoCount = Mathf.Clamp(ammoCount + ammoResupply, 0, maxAmmoCapacity);

        thePlayer.SendAmmoData(ammoCount);

        onAmmoChange.Invoke();
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }

    public int GetMaxAmmoCount()
    {
        return maxAmmoCapacity;
    }
}
