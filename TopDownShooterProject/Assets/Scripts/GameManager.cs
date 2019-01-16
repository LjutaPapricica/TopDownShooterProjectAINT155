using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform enemies;
    public Transform enemySpawners;

    private void Update()
    {
        if (enemies != null && enemySpawners != null)
        {
            if (enemies.childCount == 0 && enemySpawners.childCount == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
