using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairPad : MonoBehaviour {

    public UnityEvent onPlayerRepairing;
    public UnityEvent onPlayerRepaired;
    public UnityEvent onPlayerLeave;

    private HealthSystem playerHealthSystem;
    private AmmoSystem playerAmmoSystem;

    public float time = 0f;
    public float timeBeforeRegen = 3f;

    public bool isPlayerPresent = false;
    public bool isPlayerRepaired, isPlayerRepairing;

    private GameObject player;

    private void Update()
    {
        if (isPlayerPresent)
        {
            if (!isPlayerRepaired)
            {
                time += Time.deltaTime;
            }

            CheckPlayerFullHealthAndAmmo();
            RegeneratePlayerHealthAndAmmo(player.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerPresent = true;
            player = other.gameObject;

            playerHealthSystem = player.GetComponent<HealthSystem>();
            playerAmmoSystem = player.GetComponentInChildren<AmmoSystem>();

            playerHealthSystem.onDamaged.AddListener(ResetTimer);
        }

    }

    private void CheckPlayerFullHealthAndAmmo()
    {
        if (playerHealthSystem != null && playerAmmoSystem != null)
        {
            int health = playerHealthSystem.GetHealth(), maxHealth = playerHealthSystem.GetMaxHealth();
            int ammo = playerAmmoSystem.GetAmmoCount(), maxAmmo = playerAmmoSystem.GetMaxAmmoCount();

            if (ammo < maxAmmo || health < maxHealth)
            {
                isPlayerRepaired = false;                
                 onPlayerRepairing.Invoke();
            }
            else
            {
                isPlayerRepaired = true;
            }
        }
    }

    private void ResetTimer()
    {
        time = 0f;
        onPlayerRepairing.Invoke();
    }

    private void RegeneratePlayerHealthAndAmmo(GameObject player)
    {
        if ((time >= timeBeforeRegen) && !isPlayerRepaired)            
        {
            time = 0f;

            playerHealthSystem.AddHealth(1000);
            playerAmmoSystem.IncreaseAmmoCount(1000);

            onPlayerRepaired.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerPresent = false;
            time = 0f;
            playerHealthSystem.onDamaged.RemoveListener(ResetTimer);
            onPlayerLeave.Invoke();
        }
    }
}
