using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    private int currentSceneIndex;
    private GameObject parentLocation;
    private Transform[] locations;


    public void LoadMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameModel.Logout();
        
    }

    public void LoadScene(string sceneName)
    {
        GameModel.sceneChanged = true;
        SceneManager.LoadScene(sceneName);
    }

    public void BuildScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadInventory(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GameModel.LoadInventoryItems();
    }

    public void LoadMap(string sceneName)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScoreBoard()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("ScoreBoard");
    }


}
