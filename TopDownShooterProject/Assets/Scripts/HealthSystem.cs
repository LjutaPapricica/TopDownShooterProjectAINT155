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
        health = startingHealth;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        onHealthChange.Invoke(health);
        onDamaged.Invoke();

        if (health < 1)
        {
            onDie.Invoke();
        }
    }

    public void AddHealth(int newHealth)
    {
        if (health + newHealth > startingHealth)
        {
            health = startingHealth;
        }
        else
        {
            health += newHealth;
        }

        onHealthChange.Invoke(health);        
    }

    public int GetMaxHealth()
    {
        return startingHealth;
    }
}
