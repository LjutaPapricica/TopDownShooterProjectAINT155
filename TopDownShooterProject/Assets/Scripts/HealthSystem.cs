using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDamagedEvent : UnityEvent<int> { }

public class HealthSystem : MonoBehaviour {

    public int health = 10;
    public UnityEvent onDie;
    public OnDamagedEvent onDamaged;

    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        onDamaged.Invoke(health);

        if (health < 1)
        {
            onDie.Invoke();
        }
    }

    public void AddHealth(int newHealth)
    {
        if (health + newHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += newHealth;
        }

        onDamaged.Invoke(health);
    }
}
