using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private int currentSceneIndex;
    public void LoadInventory()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(1);

    }

    public void LoadMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(0);
    }

    public void LoadStory()
    {
        SceneManager.LoadScene(2);
    }
    private int sceneToContinue;
    public void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        if (sceneToContinue != 0)
            SceneManager.LoadScene(sceneToContinue);
        else
            return;
    }
}
