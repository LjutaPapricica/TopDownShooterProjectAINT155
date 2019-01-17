using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Slider healthBar;
    public Slider ammoBar;
    public Slider weaponHeatBar;
    public Text scoreText;
    public Text weaponsText;

    public int playerScore = 0;
    public int maxHeatValue = 100;

    private void OnEnable()
    {
        //when the canvas is enabled add listeners (methods to execute) to events (when called)
        Player.OnUpdateHealth += UpdateHealthBar;        
        AddScore.OnSendScore += UpdateScore;
        Player.OnUpdateAmmoCount += UpdateAmmoCount;
        Player.OnUpdateWeaponHeat += UpdateWeaponHeat;

    }

    //when canvas is no longer active remove listeners from events
    private void OnDisable()
    {
        Player.OnUpdateHealth -= UpdateHealthBar;
        AddScore.OnSendScore -= UpdateScore;
        Player.OnUpdateAmmoCount -= UpdateAmmoCount;
        Player.OnUpdateWeaponHeat -= UpdateWeaponHeat;
        //when going to the next scene the score is saved from that level in playprefs
        PlayerPrefs.SetInt("Score", playerScore);
    }


    private void Start()
    {
        //playerscore at the start of the level is equal to the saved score from previous levels
        playerScore = PlayerPrefs.GetInt("Score");
        //score displayed to player
        scoreText.text = "SCORE: " + playerScore.ToString();
    }
    
    //health bar value changes depending on health value passed to it
    private void UpdateHealthBar(int health)
    {
        healthBar.value = health;
    }

    //score value passed and added to the total score 
    private void UpdateScore(int theScore)
    {
        playerScore += theScore;
        //score displayed to player
        scoreText.text = "SCORE: " + playerScore.ToString();
    }

    //ammo bar value changes depending on ammo value passed to it
    private void UpdateAmmoCount(int theAmmoCount)
    {
        ammoBar.value = theAmmoCount;
    }

    //heat bar value changes depending on heat value passed to it
    private void UpdateWeaponHeat(int theWeaponHeat)
    {
        weaponHeatBar.value = maxHeatValue - theWeaponHeat;
    }
}
