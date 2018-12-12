using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Slider healthBar;
    public Slider ammoBar;
    public Text scoreText;
    public Text weaponsText;

    public int playerScore = 0;

    private void OnEnable()
    {
        Player.OnUpdateHealth += UpdateHealthBar;        
        AddScore.OnSendScore += UpdateScore;
        Player.OnUpdateAmmoCount += UpdateAmmoCount;

    }

    private void OnDisable()
    {
        Player.OnUpdateHealth -= UpdateHealthBar;
        AddScore.OnSendScore -= UpdateScore;
        Player.OnUpdateAmmoCount -= UpdateAmmoCount;
    }

    private void UpdateHealthBar(int health)
    {
        healthBar.value = health;
    }

    private void UpdateScore(int theScore)
    {
        playerScore += theScore;
        scoreText.text = "SCORE: " + playerScore.ToString();
    }

    private void UpdateAmmoCount(int theAmmoCount)
    {
        ammoBar.value = theAmmoCount;
    }

    private void UpdateCoolDown()
    
}
