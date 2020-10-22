using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Is this a factory?

public static class GameModel
{
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
        PasswdMode result = GameModel.PasswdMode.AllBad;

        Player aPlayer = ds.getPlayer(pName);
        if (aPlayer != null)
        {
            if (aPlayer.Password == pPassword)
            {
                result = GameModel.PasswdMode.OK;
                GameModel.cPlayer = aPlayer; // << WATCHOUT THIS IS A SIDE EFFECT
                GameModel.currentLocale = GameModel.ds.GetPlayerLocation(cPlayer);
            }
            else
            {
                result = GameModel.PasswdMode.NeedPassword;
            }
        }
        else
            result = GameModel.PasswdMode.NeedName;

        return result;
    }

    public static void RegisterPlayer(string pName, string pPassword)
    {

        GameModel.cPlayer = GameModel.ds.storeNewPlayer(pName, pPassword, 0, 0);
    }
    public static void MakeGame()
    {
        GameModel.ds.CreateDB();
        Location forest, cave, cave2, beach, river, highway, ocean;
        currentLocale = new Location
        {
            Name = "Forest",
            Story = " Run!!",
            NEntryX = 0f,
            NEntryY = -1.6f,
            SEntryX = 0f,
            SEntryY = -1.6f,
            EEntryX = 0f,
            EEntryY = -1.6f,
            WEntryX = 0f,
            WEntryY = -1.6f,
        };

        // forest
        forest = currentLocale;
        GameModel.ds.storeLocation(forest);
        forest.addLocation("North", "Cave", "Lava", 0f, -1.6f, 15.76392f, -0.8335584f, 0f, -1.6f, 0f, -1.6f); //
        forest.addLocation("East", "Beach", "Sharks", 0f, -1.6f, 0f, 0f, 0f, 0f, 23.29f, 0.49f); //
        forest.addLocation("West", "Highway", "Highwaymen!", 0f, 0f, 0f, 0f, -3.58f, -1.6f, 0f, 0f); //
        //forest.AddItem(forest, "Coin", 22.18f, -0.03f);
        //forest.AddItem(forest, "Chest", 5.46f, -0.9011428f);
        //forest.AddItem(forest, "Key", new Vector3(-2.53f, -2f, 0f));
        forest.AddItem(forest, "Owlett_Monster", 9.8286f, 0.43f);
        forest.AddItem(forest, "Sword", 0, -10f);
        forest.AddItem(forest, "Gold", 0, -10f);

        // cave
        cave = forest.getLocation("North");
        cave.addLocation("South", forest);
        cave.addLocation("East", "Cave2", "Enemies?", 0f, 0f, 0f, 0f, 24.2f, 1.06f, 0f, -1.6f); //
        //cave.AddItem(cave, "Coin", new Vector3(0, 0, 0));
        cave.AddItem(cave, "Rune", 0, 0);
        cave.AddItem(cave, "Chest", 0, 0);
        //cave.AddItem(cave, "Key", 0, 0);

        // cave 2
        cave2 = cave.getLocation("East");
        cave2.addLocation("West", cave);
        cave2.addLocation("East", "River", "Bridge", 0f, 0f, 0f, -1.6f, 0f, -1.6f, 24.36617f, 1.045399f); //
        river = cave2.getLocation("East");
        cave2.AddItem(cave2, "Coin", 0, 0);
        cave2.AddItem(cave2, "Rune", 0, 0);
        //cave2.AddItem(cave2, "Chest", 0, 0);
        cave2.AddItem(cave2, "Key", 0, 0);


        // beach
        beach = forest.getLocation("East");
        beach.addLocation("West", forest);
        beach.addLocation("North", "Ocean", "I guess you're amphibious", 0f, -1.6f, 19.09783f, -0.861194f, 0f, 0f, 24.36617f, 1.045399f);
        ocean = beach.getLocation("North");
        beach.AddItem(beach, "Coin", 0, 0);
        beach.AddItem(beach, "Rune", 0, 0);
        beach.AddItem(beach, "Chest", 0, 0);
        //beach.AddItem(beach, "Key", 0, 0));

        // river
        river.addLocation("West", cave2);
        river.addLocation("South", ocean);
        river.AddItem(river, "Coin", 0, 0);
        //river.AddItem(river, "Rune", 0, 0);
        river.AddItem(river, "Chest", 0, 0);
        river.AddItem(river, "Key", 0, 0);

        // ocean
        ocean.addLocation("North", river);
        ocean.addLocation("South", beach);
        ocean.AddItem(ocean, "Coin", 0, 0);
        //ocean.AddItem(ocean, "Rune", 0, 0);
        //ocean.AddItem(ocean, "Chest", 0, 0);
        ocean.AddItem(ocean, "Key", 0, 0);

        //Highway
        highway = forest.getLocation("West");
        highway.addLocation("East", forest);
        highway.AddItem(highway, "Coin", 0, 0);
        //highway.AddItem(highway, "Rune", 0, 0);
        highway.AddItem(highway, "Chest", 0, 0);
        //highway.AddItem(highway, "Key", 0, 0);

        nextLocale = currentLocale;
    }

    public static void AddItem()
    {
        // Find first open slot in inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Id == 0)
            {
                inventory[i] = items[itemToAdd];
                Debug.Log(inventory[i].Name + " was added!");
                itemAdded = true;
                break;
            }

            // If inventory was full
            if (!itemAdded)
            {
                Debug.Log("Inventory Full - Item not Added");
            }

        }
    }

    public static void LoadInventoryItems()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Id != 0)
            {
                invText = GameObject.Find("ItemText" + (Convert.ToString(i + 1))).GetComponent<Text>();
                invText.text = inventory[i].Name;
                GameObject invItemImage = GameObject.Find("ItemImage" + (Convert.ToString(i + 1)));
                invItemImage.GetComponent<Image>().sprite = Resources.Load("ItemPrefabs/" + inventory[i].Name) as Sprite;
            }
            else
            {
                break;
            }
        }
    }

    public static void UpdateDisplay()
    {
        storyHead.text = currentLocale.Name;
        storyNarrative.text = currentLocale.Story;
    }
}

