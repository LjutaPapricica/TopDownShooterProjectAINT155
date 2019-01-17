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
        //if the player is on the repair pad
        CheckIfPlayerIsPresent();
    }

    private void CheckIfPlayerIsPresent()
    {
        if (isPlayerPresent)
        {
            //if the player does not have full health or full ammo then add to timer
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
        //if the gameobject entering the collider of the repair pad is the player then the player is on the pad
        if (other.gameObject.tag == "Player")
        {
            isPlayerPresent = true;
            player = other.gameObject;

            //gets health system and ammo system components from the player tank
            playerHealthSystem = player.GetComponent<HealthSystem>();
            playerAmmoSystem = player.GetComponentInChildren<AmmoSystem>();

            //adds listener to onDamaged event so if player is damaged timer is reset
            //this means that the player must remain on the pad for a fixed amount of time without getting damaged
            playerHealthSystem.onDamaged.AddListener(ResetTimer);
        }

    }

    private void CheckPlayerFullHealthAndAmmo()
    {
        //if the health system and ammo system components are there
        if (playerHealthSystem != null && playerAmmoSystem != null)
        {
            //gets current health, max health, current ammo and max ammo
            int health = playerHealthSystem.GetHealth(), maxHealth = playerHealthSystem.GetMaxHealth();
            int ammo = playerAmmoSystem.GetAmmoCount(), maxAmmo = playerAmmoSystem.GetMaxAmmoCount();

            //if the player has less ammo or health than maximum values then the player needs repairing
            //player repairing event triggered to change state of the repair pad
            if (ammo < maxAmmo || health < maxHealth)
            {
                isPlayerRepaired = false;                
                 onPlayerRepairing.Invoke();
            }
            //else if the player has full ammo and health then no repairs are required
            else
            {
                isPlayerRepaired = true;
            }
        }
    }

    //timer until player can be repaired is reset if the player is damaged while on the pad
    private void ResetTimer()
    {
        time = 0f;
        onPlayerRepairing.Invoke();
    }

    private void RegeneratePlayerHealthAndAmmo(GameObject player)
    {
        //if the time on the repair pad is above the threshold and the player is not repaired
        //player is returned to full health and ammo
        if ((time >= timeBeforeRegen) && !isPlayerRepaired)            
        {
            //timer set back to 0
            time = 0f;

            playerHealthSystem.AddHealth(1000);
            playerAmmoSystem.IncreaseAmmoCount(1000);

            //player repaired event is invoked changing state of the repair pad
            onPlayerRepaired.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if the gameobject leaves the repair pad collider is the player then the player
        //is no longer on the repair pad. Timer set back to zero.
        if (other.gameObject.tag == "Player")
        {
            isPlayerPresent = false;
            time = 0f;
            //listener removed because repair pad no longer needs to know if the player has been damaged
            playerHealthSystem.onDamaged.RemoveListener(ResetTimer);
            //player leaving event invoked to cange state of the repair pad
            onPlayerLeave.Invoke();
        }
    }
}
