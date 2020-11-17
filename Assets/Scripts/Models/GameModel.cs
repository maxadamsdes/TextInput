using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// Is this a factory?

public static class GameModel
{
    public static JSONDropService jsDrop = new JSONDropService { Token = "e6e438ba-8877-411a-bdc2-f4270043413c" };
    public static Location currentLocale = new Location();
    public static Location nextLocale = new Location();
    public static LocationItems locationItems = new LocationItems();
    public static LoadLevel loadLevel = new LoadLevel();
    public static GameObject currentPlayer = new GameObject();
    public static GameObject currentIntObj = new GameObject();
    public static Event currentEvent;
    public static bool itemAdded = false;
    public static bool pickUpAble = false;
    public static Text storyHead;
    public static Text storyNarrative;
    public static Camera m_OrthographicCamera;
    public static GameObject menuButton;
    public static GameObject input;
    public static InputField textInput;
    public static GameObject joysticks;
    public static MenuController menuController = new MenuController();
    private static Text invText;
    public static Dictionary<int, Vector3> savedPositions = new Dictionary<int, Vector3>();
    public static string nextLocation;
    public static int itemToAdd;
    public static bool sceneChanged = true;
    public static bool finished;
    public static List<Items> items = new List<Items>() {
        new Items(){ Id = 1, Name="Coin" },
        new Items(){ Id = 2, Name="Rune" },
        new Items(){ Id = 3, Name="Key" },
        new Items(){ Id = 4, Name="Sword" }
    };
    public static List<Items> inventory = new List<Items>(10) {
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
        new Items(){ Id = 0, Name="Empty" },
    };
    public static void DestroyGameObject(GameObject obj, float t = 0.0F) { }
    public static bool DoorOpened = false;
    static String _name;
    public static string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }

    }
    static String _event;
    public static string Event
    {
        get
        {
            return _event;
        }
        set
        {
            _event = value;
        }
    }
    public static Player cPlayer = null;
    public static Location startLocation;
    public static DataService ds = new DataService("VexedText.db");
    public static Map gameMap;
    public static PopulateScoreBoard populateScoreBoard;

    // enum type for value that is one of these.
    // Here enum is being used to determine 
    // Login Reg statuses.
    public enum PasswdMode
    {
        NeedName,
        NeedPassword,
        OK,
        AllBad
    }

    public static PasswdMode CheckPassword(string pName, string pPassword)
    {
        PasswdMode result = PasswdMode.AllBad;

        Player aPlayer = ds.getPlayer(pName);
        if (aPlayer != null)
        {
            if (aPlayer.Password == pPassword)
            {
                result = PasswdMode.OK;
                cPlayer = aPlayer; // << WATCHOUT THIS IS A SIDE EFFECT
                currentLocale = ds.GetPlayerLocation(cPlayer);
                List<Player> PlayerList = new List<Player>();
                PlayerList.Add(cPlayer);
                ds.jsnGetPlayer(PlayerList);
            }
            else
            {
                result = PasswdMode.NeedPassword;
            }
        }
        else
            result = PasswdMode.NeedName;

        return result;
    }

    public static void RegisterPlayer(string pName, string pPassword)
    {
        cPlayer = ds.storeNewPlayer(pName, pPassword, "Forest", 0);
        currentLocale = ds.GetPlayerLocation(cPlayer);
        List<Player> PlayerList = new List<Player>();
        PlayerList.Add(cPlayer);
        ds.jsnStorePlayer(PlayerList);
        GameModel.ds.AddPlayerItems(cPlayer);
    }
    public static void MakeGame()
    {
        GameModel.ds.CreateDB();
        Location forest, cave, cave2, beach, river, house, ocean;
        currentLocale = new Location
        {
            Name = "Forest",
            Story = " This area seems rather pleasant. Who's that strange looking figure on that hill?"
        };

        // forest
        forest = currentLocale;
        GameModel.ds.storeLocation(forest);
        forest.addLocation("North", "Cave", "Where's the entrance gone! It looks like I'll need to cross that lava lake!", 0f, -1.6f, 15.76392f, -0.8335584f, 0f, -1.6f, 0f, -1.6f); 
        forest.addLocation("East", "Beach", "Huh, is that someone way over on the far side? I may or not not be able to reach them from here", 0f, 0.49f, 0f, 0f, 6.7f, -4.3f, 6.7f, -4.3f); 
        forest.addLocation("West", "House", "Well. This is a boring looking area", 0f, 0f, 0f, 0f, -3.58f, -1f, 0f, 0f); 

        // cave
        cave = forest.getLocation("North");
        cave.addLocation("South", forest);
        cave.addLocation("East", "Cave2", "Another lava lake... I think I can see light coming from the tunnel on the other side though!", 0f, 0f, 0f, 0f, 24.2f, 1.06f, 0f, -1.6f);

        // cave 2
        cave2 = cave.getLocation("East");
        cave2.addLocation("West", cave);
        cave2.addLocation("East", "River", "This is much nicer, I wonder what that sign says though?", 0f, 0f, 0f, -1.6f, 0f, -1.6f, 24.36617f, 1.045399f); //
        river = cave2.getLocation("East");


        // beach
        beach = forest.getLocation("East");
        beach.addLocation("West", forest);
        beach.addLocation("North", "Ocean", "I guess you're amphibious! Surely there must be treasure hidden around here, but then how are you going to get out!", 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
        ocean = beach.getLocation("North");

        // river
        river.addLocation("West", cave2);
        river.addLocation("South", ocean);

        // ocean
        ocean.addLocation("North", river);
        ocean.addLocation("South", beach);

        //House
        house = forest.getLocation("West");
        house.addLocation("East", forest);

        //currentLocale = forest;
        nextLocale = currentLocale;

        jsDrop.Store<Location, JsnReceiver>(new List<Location>
        {
            forest,
            cave,
            cave2,
            beach,
            river,
            ocean,
            house

        }, ds.jsnReceiverDel);


    }

    public static void LoadInventoryItems()
    {
        List<Items> inventory = new List<Items>();
        inventory = ds.GetInventory(GameModel.cPlayer);
        int x = 1;
        foreach(Items i in inventory)
        {
            try
            {
                Debug.Log("inventory item is " + i.Name);
                invText = GameObject.Find("ItemText" + (Convert.ToString(x))).GetComponent<Text>();
                invText.text = i.Name;
                GameObject invItemImage = GameObject.Find("ItemImage" + (Convert.ToString(x)));
                invItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("InvItems/" + i.Name);
                x += 1;
            }
            catch
            {

            }
            
        }
    }

    public static void UpdateDisplay()
    {
        storyHead.text = currentLocale.Name;
        storyNarrative.text = currentLocale.Story;
    }

    public static void Logout()
    {
        SpawnItems.UnloadGameObjects();
        if (finished)
        {
            ds.DropItems();
            SceneManager.LoadScene(0);
        }
            
        
        
    }
}

