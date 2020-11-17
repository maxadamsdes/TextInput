using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel
{ 
    public Transform[] locations;
    public GameObject parentLocation;

    public void Awake()
    {
        parentLocation = GameObject.Find("Locations");
        parentLocation.GetComponentsInChildren<Transform>(true);
        locations = parentLocation.GetComponentsInChildren<Transform>(true);
        GameModel.storyNarrative = GameObject.Find("StoryNarrative").GetComponent<Text>();
    }

    // Is called when a player changes location to load/unload the correct assets.
    public void LoadLocation()
    {
        if (GameModel.sceneChanged == true)
        {
            for (int i = 0; i < locations.Length; i++)
            {
                if (locations[i].name == GameModel.nextLocale.Name)
                {
                    
                    //unloads the items from the previous scene
                    SpawnItems.UnloadGameObjects();
                    //sets the next locations assets to be active
                    locations[i].gameObject.SetActive(true);
                    //sets the current locations assets to inactive
                    GameObject.Find(GameModel.currentLocale.Name).SetActive(false);
                    // Failsafe to tell the game that a scene has changed
                    GameModel.sceneChanged = false;
                    GameModel.currentLocale = GameModel.nextLocale;
                    GameModel.cPlayer.LocationId = GameModel.currentLocale.Name;
                    GameModel.ds.updatePlayer(GameModel.cPlayer);
                    PlayerSpawn(GameModel.currentPlayer);
                    SpawnItems.LoadGameObjects();
                    GameModel.menuController.exitText();
                    break;
                }
            }
        }
        else
        {
            GameModel.sceneChanged = true;
        }
        
    }

    private void PlayerSpawn(GameObject pPlayer)
    {
        GameObject spawn = new GameObject();
        // Finds the direction the player is coming from to then change that players position to match the scene
        if (GameModel.nextLocation == "North")
        {
            spawn = GameObject.Find("South");
            pPlayer.GetComponent<Transform>().position = spawn.GetComponent<Transform>().position;
            //GameModel.currentPlayer.GetComponent<Transform>().localPosition = new Vector3(GameModel.currentLocale.NEntryX, GameModel.currentLocale.NEntryY, 0f);
        }
        else if (GameModel.nextLocation == "South")
        {
            spawn = GameObject.Find("North");
            pPlayer.GetComponent<Transform>().position = spawn.GetComponent<Transform>().position;
            //GameModel.currentPlayer.GetComponent<Transform>().localPosition = new Vector3(GameModel.currentLocale.SEntryX, GameModel.currentLocale.SEntryY, 0f);
        }
        else if (GameModel.nextLocation == "East")
        {
            spawn = GameObject.Find("West");
            pPlayer.GetComponent<Transform>().position = spawn.GetComponent<Transform>().position;
            //GameModel.currentPlayer.GetComponent<Transform>().localPosition = new Vector3(GameModel.currentLocale.WEntryX, GameModel.currentLocale.WEntryY, 0f);
        }
        else if (GameModel.nextLocation == "West")
        {
            spawn = GameObject.Find("East");
            pPlayer.GetComponent<Transform>().position = spawn.GetComponent<Transform>().position;
            //GameModel.currentPlayer.GetComponent<Transform>().localPosition = new Vector3(GameModel.currentLocale.EEntryX, GameModel.currentLocale.EEntryY, 0f);
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
        SpawnItems.LoadGameObjects();

    }
    
}
