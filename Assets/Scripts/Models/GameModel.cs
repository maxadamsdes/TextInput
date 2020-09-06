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
    public static LoadLevel loadLevel = new LoadLevel();
    public static GameObject currentPlayer = new GameObject();
    public static GameObject currentIntObj = new GameObject();
    public static Event currentEvent;
    public static bool itemAdded = false;
    public static bool pickUpAble = false;
    public static Text storyHead;
    public static Text storyNarrative;
    public static MenuController menuController = new MenuController();
    private static Text invText;
    public static Dictionary<int, Vector3> savedPositions = new Dictionary<int, Vector3>();
    public static string nextLocation;
    public static int itemToAdd;
    public static bool sceneChanged = true;

    public static List<Items> items = new List<Items>() {
        new Items(){ Id = 1, Name="Coin", Icon= Resources.Load<Sprite>("Coin") },
        new Items(){ Id = 2, Name="Rune", Icon= Resources.Load<Sprite>("Rune") },
        new Items(){ Id = 3, Name="Key", Icon= Resources.Load<Sprite>("Key") },
        new Items(){ Id = 4, Name="Sword", Icon= Resources.Load<Sprite>("Sword") }
    };
    public static List<Items> inventory = new List<Items>(10) {
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
        new Items(){ Id = 0, Name="Empty", Icon= Resources.Load<Sprite>("Empty") },
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

    public static void MakeGame()
    {
        Location forest, cave, cave2, beach, river, highway, ocean;
        currentLocale = new Location
        {
            Name = "Forest",
            Story = " Run!!",
            NEntry = new Vector3(0f, -1.6f, 0f),
            SEntry = new Vector3(0f, -1.6f, 0f),
            EEntry = new Vector3(0f, -1.6f, 0f),
            WEntry = new Vector3(0f, -1.6f, 0f)
        };

        // forest
        forest = currentLocale;
        forest.addLocation("North", "Cave", "Lava", new Vector3(0f, -1.6f, 0f), new Vector3(15.76392f, -0.8335584f, 0f), new Vector3(0f, -1.6f, 0f), new Vector3(0f, -1.6f, 0f)); //
        forest.addLocation("East", "Beach", "Sharks", new Vector3(0f, -1.6f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(23.29f, 0.49f, 0f)); //
        forest.addLocation("West", "Highway", "Highwaymen!", new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(-3.58f, -1.6f, 0f), new Vector3(0f, 0f, 0f)); //
        forest.AddItem("Coin", new Vector3(22.18f, -0.03f, 0f));
        forest.AddItem("Chest", new Vector3(5.46f, -0.9011428f, 1f));
        forest.AddItem("Key", new Vector3(-2.53f, -2f, 0f));
        forest.AddItem("Owlett_Monster", new Vector3(9.8286f, 0.43f, 0f));
        forest.AddItem("Sword", new Vector3(0, -10f, 0f));
        forest.AddItem("Gold", new Vector3(0, -10f, 0f));

        // cave
        cave = forest.getLocation("North");
        cave.addLocation("South", forest);
        cave.addLocation("East", "Cave2", "Enemies?", new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(24.2f, 1.06f, 0f), new Vector3(0f, -1.6f, 0f)); //
        cave.AddItem("Coin", new Vector3(0, 0, 0));
        cave.AddItem("Rune", new Vector3(0, 0, 0));
        cave.AddItem("Chest", new Vector3(0, 0, 0));
        cave.AddItem("Key", new Vector3(0, 0, 0));

        // cave 2
        cave2 = cave.getLocation("East");
        cave2.addLocation("West", cave);
        cave2.addLocation("East", "River", "Bridge", new Vector3(0f, 0f, 0f), new Vector3(0f, -1.6f, 0f), new Vector3(0f, -1.6f, 0f), new Vector3(24.36617f, 1.045399f, 0f)); //
        river = cave2.getLocation("East");
        cave2.AddItem("Coin", new Vector3(0, 0, 0));
        cave2.AddItem("Rune", new Vector3(0, 0, 0));
        cave2.AddItem("Chest", new Vector3(0, 0, 0));
        cave2.AddItem("Key", new Vector3(0, 0, 0));


        // beach
        beach = forest.getLocation("East");
        beach.addLocation("West", forest);
        beach.addLocation("North", "Ocean", "I guess you're amphibious", new Vector3(0f, -1.6f, 0f), new Vector3(19.09783f, -0.8661194f, 0f), new Vector3(0f, 0f, 0f), new Vector3(24.36617f, 1.045399f, 0f));
        ocean = beach.getLocation("North");
        beach.AddItem("Coin", new Vector3(0, 0, 0));
        beach.AddItem("Rune", new Vector3(0, 0, 0));
        beach.AddItem("Chest", new Vector3(0, 0, 0));
        beach.AddItem("Key", new Vector3(0, 0, 0));

        // river
        river.addLocation("West", cave2);
        river.addLocation("South", ocean);
        river.AddItem("Coin", new Vector3(0, 0, 0));
        river.AddItem("Rune", new Vector3(0, 0, 0));
        river.AddItem("Chest", new Vector3(0, 0, 0));
        river.AddItem("Key", new Vector3(0, 0, 0));

        // ocean
        ocean.addLocation("North", river);
        ocean.addLocation("South", beach);
        ocean.AddItem("Coin", new Vector3(0, 0, 0));
        ocean.AddItem("Rune", new Vector3(0, 0, 0));
        ocean.AddItem("Chest", new Vector3(0, 0, 0));
        ocean.AddItem("Key", new Vector3(0, 0, 0));

        //Highway
        highway = forest.getLocation("West");
        highway.addLocation("East", forest);
        highway.AddItem("Coin", new Vector3(0, 0, 0));
        highway.AddItem("Rune", new Vector3(0, 0, 0));
        highway.AddItem("Chest", new Vector3(0, 0, 0));
        highway.AddItem("Key", new Vector3(0, 0, 0));
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
                invItemImage.GetComponent<Image>().sprite = inventory[i].Icon;
            }
            else
            {
                break;
            }
        }
    }
}

