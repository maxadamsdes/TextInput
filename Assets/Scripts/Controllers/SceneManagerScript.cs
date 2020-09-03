using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private int currentSceneIndex;
    private GameObject parentLocation;
    private Transform[] locations;
    public void LoadInventory()
    {
        SaveState();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(1);
        GameModel.LoadInventoryItems();
    }

    public void LoadMenu()
    {
        SaveState();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(0);
    }

    public void LoadStory()
    {
        SceneManager.LoadScene(2);
        parentLocation = GameObject.Find("Locations");
        locations = parentLocation.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].name == GameModel.currentLocale.Name)
            {
                //playerObj.GetComponent<Transform>().position = new Vector3(0f, -1.47f, 0);
                locations[i].gameObject.SetActive(true);
                GameModel.LoadGame();
                break;
            }
            locations[i].gameObject.SetActive(false);
        }
    }
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

}
