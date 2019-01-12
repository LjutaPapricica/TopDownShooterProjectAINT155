using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairPad : MonoBehaviour {

    public float time = 0f;
    public float timeBeforeRegen = 3f;

    public bool isPlayerPresent = false;
    private GameObject player;

    private void Update()
    {
        if (isPlayerPresent)
        {
            time += Time.deltaTime;

            RegeneratePlayerHealthAndAmmo(player.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerPresent = true;
            player = other.gameObject;
            player.GetComponent<HealthSystem>().onDamaged.AddListener(ResetTimer);
        }

    }

    private void ResetTimer()
    {
        time = 0f;
    }

    private void RegeneratePlayerHealthAndAmmo(GameObject player)
    {
        if (time >= timeBeforeRegen)
        {
            time = 0f;

            player.GetComponent<HealthSystem>().AddHealth(1000);
            player.GetComponentInChildren<AmmoSystem>().IncreaseAmmoCount(1000);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerPresent = false;
            time = 0f;
        }
    }
}
