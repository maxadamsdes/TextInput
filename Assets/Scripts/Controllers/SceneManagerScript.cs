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

    public void LoadInventory()
    {
        SaveState();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SaveState();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GameObject Player = GameObject.Find("Player");
        //Player.GetComponent<Transform>().position.x = GameModel.playerPositionx;
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


    //private int sceneToContinue;

    //public void ContinueGame()
    //{;
    //    if (GameModel.currentLocale != null)
    //        SceneManager.LoadScene(sceneToContinue);
    //    else
    //        return;
    //}

    public void SaveState()
    {
        GameModel.currentPlayer = GameObject.Find("Player");
    }

    public void LoadInventoryTest()
    {
        //GameModel.Test = GameModel.Test + 1;
        //GameObject.Find("Testies").GetComponent<Text>().text = Convert.ToString(GameModel.Test);
        GameModel.LoadInventoryItems();
    }
}
