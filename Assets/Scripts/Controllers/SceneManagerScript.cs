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
        SceneManager.LoadScene(0);
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

    //public void LoadStory()
    //{
    //    SceneManager.LoadScene(2);
    //    parentLocation = GameObject.Find("Locations");
    //    locations = parentLocation.GetComponentsInChildren<Transform>(true);
    //    for (int i = 0; i < locations.Length; i++)
    //    {
    //        if (locations[i].name == GameModel.Instance.currentLocale.Name)
    //        {
    //            //playerObj.GetComponent<Transform>().position = new Vector3(0f, -1.47f, 0);
    //            locations[i].gameObject.SetActive(true);
    //            GameModel.Instance.LoadGame();
    //            break;
    //        }
    //        locations[i].gameObject.SetActive(false);
    //    }
    //}


    // To implement after save file functionality
    //public void ContinueGame()
    //{
    //    if (GameModel.currentLocale != null)
    //        SceneManager.LoadScene(1);
    //    else
    //        return;
    //}

}
