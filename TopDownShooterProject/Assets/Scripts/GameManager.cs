using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform enemies;
    public Transform enemySpawners;

    private void Update()
    {
        CheckIfEnemiesPresent();
    }

    private void CheckIfEnemiesPresent()
    {
        //if there is an enemies transform and enemySpawners transform
        if (enemies != null && enemySpawners != null)
        {
            // if the number of enemy tanks under the enemies transform and the number of enemy factories under the enemySpawners transform is zero
            //then load the gamescene as the player has completed the objectives for this level
            if (enemies.childCount == 0 && enemySpawners.childCount == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    //when the start game button is clicked the first level is loaded
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    //when the player is killed the game over scene is loaded
    public void EndGame()
    {
        SceneManager.LoadScene("Game Over");
    }

    //loads menu scene
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //quits the application
    public void Quit()
    {
        Application.Quit();
    }
}
