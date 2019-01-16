using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHeatSystem : MonoBehaviour {

    private Player thePlayer;

    public int currentHeat = 0;
    public int maxHeatThreshold = 100;

    public bool isCoolingDown = false;
    private bool resetHeatMeter = false;
    private bool canWeaponFire = true;

    public float time = 0f;
    public float coolDownRate = 0.01f;
    public float coolDownTimeDelay = 3f;

    // Use this for initialization
    void Start ()
    {
        thePlayer = transform.parent.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        WeaponCoolDown();
    }

    public void IncreaseWeaponHeat(int heatValue)
    {
        currentHeat = Mathf.Clamp(currentHeat + heatValue, 0, maxHeatThreshold);
        time = 0;

        thePlayer.SendWeaponHeatData(currentHeat);
    }

    private void WeaponCoolDown()
    {
        if (time > coolDownTimeDelay && !isCoolingDown)
        {
            isCoolingDown = true;
            StartCoroutine(WeaponCooling());
        }

        thePlayer.SendWeaponHeatData(currentHeat);
    }

    IEnumerator WeaponCooling()
    {
        currentHeat = Mathf.Clamp(currentHeat - 1, 0, maxHeatThreshold);
        yield return new WaitForSeconds(coolDownRate);
        isCoolingDown = false;
    }

    public bool GetWeaponHeat()
    {
        if (currentHeat >= maxHeatThreshold)
        {
            resetHeatMeter = true;
            canWeaponFire = false;
        }
        else if (!resetHeatMeter)
        {
            canWeaponFire = true;
        }

        if (resetHeatMeter && currentHeat == 0)
        {
            resetHeatMeter = false;
        }

        return canWeaponFire;
    }
    
}
