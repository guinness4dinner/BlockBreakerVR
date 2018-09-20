using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextLevel()
    {
           int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
           if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
           {
               SceneManager.LoadScene(currentSceneIndex + 1);
           }
           else
           {
               LoadMainMenu();
           } 
    }

    public void LoadLevel2Debug()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
        SceneManager.LoadScene(0);      
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");      
    }

}
