using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Is this a factory?

public static class GameModel
{

    public static Location currentLocale;
    public static Location nextLocale;
    public static GameObject currentPlayer;
    public static Event currentEvent;
    public static GameObject[] inventory = new GameObject[10];
    public static GameObject[] itemList;
    public static bool itemAdded = false;
    private static GameObject invItemImage;
    private static GameObject invItemName;
    public static Vector3 playerPosition;
    public static Text storyHead;
    public static Text storyNarrative;
    static MenuController menuController;
    public static int Test = 0;
    public static Vector3 playerPositionx;
    public static Vector3 playerPositiony;
    public static Vector3 playerPositionz;

    static String _name;
	public static string Name
    {
		get 
		{ 
			return _name;  
		}
		set{
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
            Story = " Run!!"
        };

        // forest
        forest = currentLocale;
        forest.addLocation("North", "Cave", "Lava");
        forest.addLocation("East", "Beach", "Sharks");
        forest.addLocation("West", "Highway", "Highwaymen!");

        // cave
        cave = forest.getLocation("North");
        cave.addLocation("South", forest);
        cave.addLocation("East", "Cave2", "Enemies?");

        // cave 2
        cave2 = cave.getLocation("East");
        cave2.addLocation("West", cave);
        cave2.addLocation("East", "River", "Bridge");
        river = cave2.getLocation("East");

        // beach
        beach = forest.getLocation("East");
        beach.addLocation("West", forest);
        beach.addLocation("East", "Ocean", "I guess you're amphibious");
        ocean = beach.getLocation("East");

        // river
        river.addLocation("West", cave2);
        river.addLocation("South", ocean);

        // ocean
        ocean.addLocation("North", river);
        ocean.addLocation("West", beach);

        //Highway
        highway = forest.getLocation("West");
        highway.addLocation("East", forest);
    }

    public static void AddItem(GameObject item)
    {
        // Find first open slot in inventory
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                Debug.Log(item.name + " was added!");

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
        for (int i = 0; i < 10; i++)
        {
            string invName = inventory[i].name;
            if (inventory[i] != null)
            {
                invItemImage = GameObject.Find("ItemImage" + (Convert.ToString(i + 1)));
                //invItemImage.GetComponent<Image>().sprite = GameModel.inventory[0].GetComponent<Image>().sprite;
                //invItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Assets/Sprites/Items/" + inventory[i].name);
                //invItemName = GameObject.Find("ItemText" + (Convert.ToString(i + 1)));
                GameObject.Find("ItemText1").GetComponent<Text>().text = "test";
                //invItemName.GetComponent<Text>().text = GameModel.inventory[i].name;
            }
            else
            {
                //break;
            }
        }
    }

    public static void LoadGame()
    {
        GameObject.Find(currentLocale.Name).SetActive(false);
        if (nextLocale != null)
        {
            currentLocale = nextLocale;
        }
        //currentPlayer.transform.position = new Vector3(0f, -1.47f, 0);
        storyHead.text = currentLocale.Name;
        storyNarrative.text = currentLocale.Story;
        menuController.exitText();
    }
}

