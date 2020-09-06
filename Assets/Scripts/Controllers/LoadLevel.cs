using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{ 
    public Transform[] locations;
    public GameObject parentLocation;

    public void Awake()
    {
        parentLocation = GameObject.Find("Locations");
        parentLocation.GetComponentsInChildren<Transform>(true);
        locations = parentLocation.GetComponentsInChildren<Transform>(true);
    }

    public void LoadLocation()
    {
        parentLocation = GameObject.Find("Locations");
        parentLocation.GetComponentsInChildren<Transform>(true);
        locations = parentLocation.GetComponentsInChildren<Transform>(true);
        GameModel.currentPlayer = GameObject.Find("Player");
        for (int i = 0; i < locations.Length; i++)
        {
            if (GameModel.sceneChanged != true)
            {
                if (locations[i].name == GameModel.nextLocale.Name)
                {
                    if (GameModel.nextLocation == "North")
                        GameModel.currentPlayer.GetComponent<Transform>().position = GameModel.currentLocale.NEntry;
                    else if (GameModel.nextLocation == "South")
                        GameModel.currentPlayer.GetComponent<Transform>().position = GameModel.currentLocale.SEntry;
                    else if (GameModel.nextLocation == "East")
                        GameModel.currentPlayer.GetComponent<Transform>().position = GameModel.currentLocale.EEntry;
                    else if (GameModel.nextLocation == "West")
                        GameModel.currentPlayer.GetComponent<Transform>().position = GameModel.currentLocale.WEntry;
                    locations[i].gameObject.SetActive(true);
                    GameObject.Find(GameModel.currentLocale.Name).SetActive(false);
                    LoadGame();
                    break;
                }
            }
            else
            {
                if (locations[i].name == GameModel.currentLocale.Name)
                {
                    locations[i].gameObject.SetActive(true);
                    GameModel.sceneChanged = false;
                    LoadGame();
                    break;
                }
            }
            
        }
    }

    public void LoadGame()
    {

        if (GameModel.nextLocale != null)
        {
            SpawnItems.DestroyGameObjects();
            GameModel.currentLocale = GameModel.nextLocale;
            SpawnItems.LoadGameObjects();
        }
        GameModel.storyHead.text = GameModel.currentLocale.Name;
        GameModel.storyNarrative.text = GameModel.currentLocale.Story;
        GameModel.menuController.exitText();
    }
}
