using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour {

    public float time = 1;
    public bool repeat = false;
    public UnityEvent onTimerComplete;

    private void Start()
    {
        //if repeating enabled then event is called at a fixed time interval
        if (repeat)
        {
            InvokeRepeating("OnTimerComplete", 0, time);
        }
        //else event is invoked only once after a set time period
        else
        {
            Invoke("OnTimerComplete", time);
        }
    }

    //when timer is complete invoke the time completed event 
    private void OnTimerComplete()
    {
        onTimerComplete.Invoke();
    }
}
