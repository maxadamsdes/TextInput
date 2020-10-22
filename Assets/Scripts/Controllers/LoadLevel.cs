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

    // Is called when a player changes location to load/unload the correct assets.
    public void LoadLocation()
    {
        
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].name == GameModel.nextLocale.Name)
            {
                // Finds the direction the player is coming from to then change that players position to match the scene
                if (GameModel.nextLocation == "North")
                    GameModel.currentPlayer.GetComponent<Transform>().position = new Vector3(GameModel.currentLocale.NEntryX, GameModel.currentLocale.NEntryY, 0f);
                else if (GameModel.nextLocation == "South")
                    GameModel.currentPlayer.GetComponent<Transform>().position = new Vector3(GameModel.currentLocale.SEntryX, GameModel.currentLocale.SEntryY, 0f);
                else if (GameModel.nextLocation == "East")
                    GameModel.currentPlayer.GetComponent<Transform>().position = new Vector3(GameModel.currentLocale.EEntryX, GameModel.currentLocale.EEntryY, 0f);
                else if (GameModel.nextLocation == "West")
                    GameModel.currentPlayer.GetComponent<Transform>().position = new Vector3(GameModel.currentLocale.WEntryX, GameModel.currentLocale.WEntryY, 0f);
                //sets the next locations assets to be active
                locations[i].gameObject.SetActive(true);
                //sets the current locations assets to inactive
                GameObject.Find(GameModel.currentLocale.Name).SetActive(false);
                // Failsafe to tell the game that a scene has changed
                GameModel.sceneChanged = true;
                SpawnItems.DestroyGameObjects();
                GameModel.currentLocale = GameModel.nextLocale;
                GameModel.cPlayer.LocationId = GameModel.currentLocale.Id;
                GameModel.ds.updatePlayerLocation(GameModel.cPlayer);
                SpawnItems.LoadGameObjects();
                GameModel.storyHead.text = "Test";//GameModel.currentLocale.Name;
                GameModel.storyNarrative.text = GameModel.currentLocale.Story;
                GameModel.menuController.exitText();
                break;
            }
        }
    }

    public void StartLocation()
    {
        parentLocation = GameObject.Find("Locations");
        parentLocation.GetComponentsInChildren<Transform>(true);
        locations = parentLocation.GetComponentsInChildren<Transform>(true);
        GameModel.currentPlayer = GameObject.Find("Player");
        // Loops through the locations transform array 
        for (int i = 0; i < locations.Length; i++)
        {
            //trys to find a game object that matches the locations name
            if (locations[i].name == GameModel.currentLocale.Name)
            {
                //When successful it sets this locations assets to active
                locations[i].gameObject.SetActive(true);
            }
        }
    }
    
}
