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
        //gets player script on the parent transform 
        thePlayer = transform.parent.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //timer increases each update
        time += Time.deltaTime;

        WeaponCoolDown();
    }

    //heat value passed to the method which is added to the total accumulated heat
    public void IncreaseWeaponHeat(int heatValue)
    {
        //the current heat cannot go below 0 and cannot exceed the max heat threshold
        currentHeat = Mathf.Clamp(currentHeat + heatValue, 0, maxHeatThreshold);
        //timer is set to zero
        time = 0;

        //sends new total heat value to the heat bar UI
        thePlayer.SendWeaponHeatData(currentHeat);
    }

    private void WeaponCoolDown()
    {
        //when the time exceeds the cool down time delay and there is no current cool down
        //then starts to cool (decrease the heat) 
        if (time > coolDownTimeDelay && !isCoolingDown)
        {
            isCoolingDown = true;
            //starts coroutine which decreases heat by 1 after a fixed time period
            StartCoroutine(WeaponCooling());
        }

        //sends new total heat value to the heat bar UI
        thePlayer.SendWeaponHeatData(currentHeat);
    }

    IEnumerator WeaponCooling()
    {
        //currentheat decreased by one at each set time interval
        //current heat cannot below 0
        currentHeat = Mathf.Clamp(currentHeat - 1, 0, maxHeatThreshold);
        yield return new WaitForSeconds(coolDownRate);
        isCoolingDown = false;
    }

    public bool GetWeaponHeat()
    {
        //if the current heat reaches the max heat threshold then the heat bar must be reset i.e go down to zero
        //weapons that produce heat cannot fire during this time
        if (currentHeat >= maxHeatThreshold)
        {
            resetHeatMeter = true;
            canWeaponFire = false;
        }
        //otherwise weapons that produce heat can still fire
        else if (!resetHeatMeter)
        {
            canWeaponFire = true;
        }

        //once the current heat has reached zero after a reset then weapons that produce heat can begin to fire again
        if (resetHeatMeter && currentHeat == 0)
        {
            resetHeatMeter = false;
        }

        //returns boolean to determine whether weapon can fire or not depending on the amount of current heat
        return canWeaponFire;
    }
    
}
