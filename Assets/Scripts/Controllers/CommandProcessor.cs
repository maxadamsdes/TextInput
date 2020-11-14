using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandProcessor
{
    GameObject menuController = GameObject.FindGameObjectWithTag("GameController");

    public CommandProcessor ()
		{
		}
    

    public String Parse(String pCmdStr){
        TriggerEvent triggered;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject storyNarrative = GameObject.FindGameObjectWithTag("Story");
        triggered = playerObj.GetComponent<TriggerEvent>();
        Animator anim = triggered.anim;
        String strResult = GameModel.currentLocale.Name;
		pCmdStr = pCmdStr.ToLower();
		String[] parts = pCmdStr.Split(' '); // tokenise the command

        if (parts.Length > 0)
        {// process the tokens
            switch (parts[0])
            {
                case "pick":
                    if (parts[1] == "up")
                    {
                        Debug.Log("Got Pick up");
                        strResult = "Got Pick up";

                        try
                        {
                            if ((GameModel.currentIntObj.tag == "Item") && (GameModel.pickUpAble == true))
                            {

                                GameModel.AddItem(GameModel.currentLocale, GameModel.cPlayer, GameModel.currentIntObj.name);
                                if (GameModel.itemAdded != true)
                                {
                                    Debug.Log("Not enough space!");
                                    strResult = "You dont have enough space for " + GameModel.currentIntObj.name + "!";
                                }
                                else
                                {
                                    Debug.Log(GameModel.currentIntObj.name + " added to Inventory");
                                    strResult = GameModel.currentIntObj.name + " added to Inventory";
                                    storyNarrative.GetComponent<Text>().text = "Sweet as";
                                    GameModel.DestroyGameObject(GameModel.currentIntObj);
                                    GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, GameModel.currentIntObj.name, GameModel.currentIntObj.GetComponent<Transform>().position.x);
                                    GameModel.currentIntObj.SetActive(false);
                                    GameModel.currentIntObj = null;
                                }

                            }
                            else
                            {
                                Debug.Log("Not able to be picked up");
                                strResult = "There is nothing to pick up";
                            }
                        }
                        catch
                        {
                            Debug.Log("Not able to be picked up");
                            strResult = "There is nothing to pick up";
                        }

                    }

                    if (parts.Length == 3)
                    {
                        String param = parts[2];
                    }// do pick up command
                     // GameModel.Pickup();

                    break;
                case "go":

                    switch (parts[1])
                    {
                        case "north":
                            Debug.Log("Got go North");
                            GameModel.nextLocale = GameModel.currentLocale.getLocation("North");
                            if (GameModel.nextLocale == null)
                                strResult = "Sorry can't go North | " + GameModel.currentLocale.Name;
                            else
                            {
                                GameModel.nextLocation = "North";
                                GameModel.sceneChanged = true;
                                GameModel.loadLevel.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                            }

                            break;
                        case "south":
                            Debug.Log("Got go South");
                            GameModel.nextLocale = GameModel.currentLocale.getLocation("South");
                            if (GameModel.nextLocale == null)
                            {
                                strResult = "Sorry can't go South | " + GameModel.currentLocale.Name;
                            }
                            else
                            {
                                GameModel.nextLocation = "South";
                                GameModel.sceneChanged = true;
                                GameModel.loadLevel.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                            }
                            break;
                        case "east":
                            Debug.Log("Got go East");
                            GameModel.nextLocale = GameModel.currentLocale.getLocation("East");
                            if (GameModel.nextLocale == null)
                            {
                                strResult = "Sorry can't go East | " + GameModel.currentLocale.Name;
                            }
                            else
                            {
                                GameModel.nextLocation = "East";
                                GameModel.sceneChanged = true;
                                GameModel.loadLevel.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                            }
                            break;
                        case "west":
                            Debug.Log("Got go West");
                            GameModel.nextLocale = GameModel.currentLocale.getLocation("West");
                            if (GameModel.nextLocale == null)
                            {
                                strResult = "Sorry can't go West | " + GameModel.currentLocale.Name;
                            }
                            else
                            {
                                GameModel.nextLocation = "West";
                                GameModel.loadLevel.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                            }
                            break;
                        default:
                            Debug.Log(" do not know how to go there");
                            strResult = "Do not know how to go there";
                            break;
                    }// end switch
                    break;

                case "test":
                    try
                    {
                        Debug.Log(GameModel.currentIntObj.name);
                    }
                    catch
                    {
                        Debug.Log("Can't help lol");
                    }  
                    break;

                case "enter":
                    if ((parts.Length == 1) && (GameModel.currentIntObj.tag == "Entrance"))
                    {
                        GameModel.nextLocale = GameModel.currentLocale.getLocation(GameModel.currentIntObj.name);
                        Debug.Log("Got go " + GameModel.currentIntObj.name);
                        if (GameModel.nextLocale == null)
                        {
                            triggered.storyHead.text = "Sorry can't go to " + GameModel.nextLocale + " | " + GameModel.currentLocale.Name;
                        }
                        else
                        {
                            GameModel.sceneChanged = true;
                            GameModel.loadLevel.LoadLocation();
                            triggered.failsafe = true;
                        }
                        strResult = GameModel.currentLocale.Name;
                    }
                    else if (parts.Length == 1)
                    {
                        Debug.Log("Not able to enter");
                        strResult = "There's nowhere to enter";
                    }
                    
                    break;
                    case "take":
                    if (parts.Length == 1)
                    {
                        try
                        {
                            if (GameModel.currentIntObj.name == "Chest")
                            {
                                //string[] itemOptions = { "CoinStatic", "KeyStatic", "RuneStatic" };
                                var rand = new System.Random();
                                var randomint = rand.Next(GameModel.items.Count);
                                var randomItem = GameModel.items[randomint];
                                GameModel.itemToAdd = randomint;
                                GameModel.AddItem(GameModel.currentLocale, GameModel.cPlayer, GameModel.currentIntObj.name);
                                if (GameModel.itemAdded != true)
                                {
                                    Debug.Log("Not enough space!");
                                    strResult = "You dont have enough space for " + randomItem.Name + "!";
                                }
                                else
                                {
                                    Debug.Log(randomItem.Name + " added to Inventory");
                                    strResult = (randomItem.Name + " added to Inventory");
                                    storyNarrative.GetComponent<Text>().text = "Nice find!";
                                    anim.SetBool("Looted", true);
                                    GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, GameModel.currentIntObj.name, GameModel.currentIntObj.GetComponent<Transform>().position.x);
                                    GameModel.currentIntObj.GetComponent<Collider2D>().enabled = false;
                                    GameModel.currentIntObj = null;
                                }
                            }
                            else
                            {
                                Debug.Log("Not able to be take");
                                strResult = "There is nothing to take";
                            }
                        }
                        catch
                        {
                            Debug.Log("Not able to be take");
                            strResult = "There is nothing to take";
                        }
                    }
                    else
                    {
                        switch (parts[1])
                        {

                            case "sword":
                                try
                                {
                                    if (GameModel.currentIntObj.name == "Owlett_Monster")
                                    {
                                        try
                                        {
                                            GameModel.itemToAdd = 4;
                                            GameModel.AddItem(GameModel.currentLocale, GameModel.cPlayer, "Sword");
                                            if (GameModel.itemAdded != true)
                                            {
                                                Debug.Log("Not enough space!");
                                                strResult = "You dont have enough space for the Sword!";
                                            }
                                            else
                                            {
                                                Debug.Log("Got take Sword");
                                                strResult = "Taken the sword!";
                                                storyNarrative.GetComponent<Text>().text = "All power to you";
                                                GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Sword", 0f);
                                                GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Gold", 0f);
                                                GameObject.Find("Sword").SetActive(false);
                                                GameObject.Find("Gold").SetActive(false);
                                            }
                                        }
                                        catch
                                        {
                                            Debug.Log("Can't do that");
                                            strResult = "You've already chosen!";
                                            
                                        }

                                    }
                                    else
                                    {
                                        Debug.Log("Not able to take");
                                        strResult = "You can't take " + parts[1] + "!";
                                    }
                                }
                                catch
                                {
                                    Debug.Log("Not able to take");
                                    strResult = "You can't take " + parts[1] + "!";
                                }


                                break;
                            case "gold":
                                try
                                {

                                    if (GameModel.currentIntObj.name == "Owlett_Monster")
                                    {
                                        try
                                        {
                                            GameModel.itemToAdd = 1;
                                            GameModel.AddItem(GameModel.currentLocale, GameModel.cPlayer, "Gold");
                                            if (GameModel.itemAdded != true)
                                            {
                                                Debug.Log("Not enough space!");
                                                strResult = "You dont have enough space for the Gold!";
                                            }
                                            else
                                            {
                                                Debug.Log("Got take Gold");
                                                strResult = "Taken the Gold!";
                                                storyNarrative.GetComponent<Text>().text = "A wise choice.";
                                                GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Sword", 0f);
                                                GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Gold", 0f);
                                                GameObject.Find("Sword").SetActive(false);
                                                GameObject.Find("Gold").SetActive(false);
                                            }
                                        }
                                        catch
                                        {
                                            Debug.Log("Can't do that");
                                            strResult = "You've already chosen!";
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("Not able to take");
                                        strResult = "You can't take " + parts[1] + "!";
                                    }

                                }
                                catch
                                {
                                    Debug.Log("Not able to take");
                                    strResult = "You can't take " + parts[1] + "!";
                                }

                                break;
                            default:
                                Debug.Log("Not able to take");
                                strResult = "You can't take " + parts[1] + "!";
                                break;
                        }
                    }
                   
            
                    break;
                case "steal":
                    try
                    {
                        if ((GameModel.currentIntObj.name == "Owlett_Monster") && (GameObject.Find("Sword").activeSelf) || (GameObject.Find("Gold").activeSelf))
                        {
                            try
                            {
                                var rand = new System.Random();
                                var randomBool = rand.Next(2) == 1;
                                if (!randomBool)
                                {
                                    GameModel.itemToAdd = 3;
                                    GameModel.AddItem(GameModel.currentLocale, GameModel.cPlayer, "Sword");
                                    if (GameModel.itemAdded != true)
                                    {
                                        Debug.Log("Not enough space!");
                                        strResult = "You dont have enough space for the Sword!";
                                    }
                                    else
                                    {
                                        GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Sword", 0f);
                                        GameObject.Find("Sword").SetActive(false);
                                    }

                                    GameModel.itemToAdd = 0;
                                    GameModel.AddItem(GameModel.currentLocale, GameModel.cPlayer, "Gold");
                                    if (GameModel.itemAdded != true)
                                    {
                                        Debug.Log("Not enough space!");
                                        strResult = "You dont have enough space for the Gold!";
                                    }
                                    else
                                    {
                                        GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Gold", 0f);
                                        GameObject.Find("Gold").SetActive(false);
                                    }

                                    Debug.Log("Got steal");
                                    strResult = "Stolen both items!";
                                    storyNarrative.GetComponent<Text>().text = "!!!";
                                    GameModel.currentIntObj.SetActive(false);
                                    GameModel.currentIntObj = null;
                                    if (parts.Length == 3)
                                    {
                                        String param = parts[2];
                                    }
                                }
                                else
                                {
                                    Debug.Log("Failed steal");
                                    strResult = "You were caught!";
                                    storyNarrative.GetComponent<Text>().text = "Naughty, naughty... Bye!";
                                    GameModel.currentIntObj.GetComponent<Text>().Equals("");
                                    GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Sword", 0f);
                                    GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Gold", 0f);
                                    GameModel.ds.Removeitem(GameModel.cPlayer.PlayerName, GameModel.currentLocale.Id, "Owlett_Monster", 0f);
                                    GameModel.currentIntObj.SetActive(false);
                                    GameModel.currentIntObj = null;
                                }
                            }
                            catch
                            {
                                Debug.Log("Can't do that");
                                strResult = "You've already stolen!";
                            }

                        }
                        else
                        {
                            Debug.Log("Not able to steal");
                            strResult = "You can't steal here";
                        }
                    }
                    catch
                    {
                        Debug.Log("Not able to steal");
                        strResult = "You can't steal here";
                    }


                    break;
                case "":
                    strResult = GameModel.currentLocale.Name;
                    break;
                default:
                        Debug.Log("Do not understand");
                        strResult = "Do not understand";
                        break;

                }// end switch
            }
            else
            {
                Debug.Log("Do not understand");
                strResult = "Do not understand";
            }

		return strResult;

		}// Parse
}


