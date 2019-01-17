using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OnHealthChangeEvent : UnityEvent<int> { }

public class HealthSystem : MonoBehaviour {

    private int health;
    public UnityEvent onDie;
    public OnHealthChangeEvent onHealthChange;
    public UnityEvent onDamaged;

    public int startingHealth = 10;

    private void Start()
    {
        //at the start health is set to maximum value
        health = startingHealth;
    }

    //damage value passed to TakeDamage method
    public void TakeDamage(int damage)
    {
        //damage deducted from health
        health -= damage;

        //when the health changes the health change event is invoked and the new health value is passed on
        onHealthChange.Invoke(health);
        //damaged event is also invoked as health has been reduced
        onDamaged.Invoke();

        //once health reaches the zero the die event is triggered and the gameobject is destroyed
        if (health < 1)
        {
            onDie.Invoke();
        }
    }

    //value passed and added to total health
    public void AddHealth(int newHealth)
    {
        //if the new total health value exceeds the maximum starting health value then health
        //is equal to the starting (max)  value
        if (health + newHealth > startingHealth)
        {
            health = startingHealth;
        }
        //otherwise add value to total health
        else
        {
            health += newHealth;
        }

        //when the health changes the health change event is invoked and the new health value is passed on
        onHealthChange.Invoke(health);        
    }

    //returns max health value
    public int GetMaxHealth()
    {
        return startingHealth;
    }

    //returns current health value
    public int GetHealth()
    {
        return health;
    }
}
