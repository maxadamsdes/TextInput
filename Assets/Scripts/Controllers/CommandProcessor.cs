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
                            if (triggered.currentInterObj.tag == "Item")
                            {

                                triggered.inventory.AddItem(triggered.currentInterObj);
                                if (triggered.inventory.itemAdded != true)
                                {
                                    Debug.Log("Not enough space!");
                                    strResult = "You dont have enough space for " + triggered.currentInterObj.name + "!";
                                }
                                else
                                {
                                    Debug.Log(triggered.currentInterObj.name + " added to Inventory");
                                    strResult = triggered.currentInterObj.name + " added to Inventory";
                                    storyNarrative.GetComponent<Text>().text = "Sweet as";
                                    triggered.currentInterObj.SetActive(false);
                                    triggered.currentInterObj = null;
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
                            triggered.nextLocale = GameModel.currentLocale.getLocation("North");
                            if (triggered.nextLocale == null)
                                strResult = "Sorry can't go North | " + GameModel.currentLocale.Name;
                            else
                            {
                                triggered.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                                //GameModel.currentLocale = nextLocale;
                                //strResult = GameModel.currentLocale.Name;
                                //storyNarrative.GetComponent<Text>().text = GameModel.currentLocale.Story;
                            }

                            break;
                        case "south":
                            Debug.Log("Got go South");
                            triggered.nextLocale = GameModel.currentLocale.getLocation("South");
                            if (triggered.nextLocale == null)
                            {
                                strResult = "Sorry can't go South | " + GameModel.currentLocale.Name;
                            }
                            else
                            {
                                triggered.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                                //GameModel.currentLocale = nextLocale;
                                //strResult = GameModel.currentLocale.Name;
                                //storyNarrative.GetComponent<Text>().text = GameModel.currentLocale.Story;
                            }
                            break;
                        case "east":
                            Debug.Log("Got go East");
                            triggered.nextLocale = GameModel.currentLocale.getLocation("East");
                            if (triggered.nextLocale == null)
                            {
                                strResult = "Sorry can't go East | " + GameModel.currentLocale.Name;
                            }
                            else
                            {
                                triggered.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                                //GameModel.currentLocale = nextLocale;
                                //strResult = GameModel.currentLocale.Name;
                                //storyNarrative.GetComponent<Text>().text = GameModel.currentLocale.Story;
                            }
                            break;
                        case "west":
                            Debug.Log("Got go West");
                            triggered.nextLocale = GameModel.currentLocale.getLocation("West");
                            if (triggered.nextLocale == null)
                            {
                                strResult = "Sorry can't go West | " + GameModel.currentLocale.Name;
                            }
                            else
                            {
                                triggered.LoadLocation();
                                strResult = GameModel.currentLocale.Name;
                                //GameModel.currentLocale = nextLocale;
                                //strResult = GameModel.currentLocale.Name;
                                //storyNarrative.GetComponent<Text>().text = GameModel.currentLocale.Story;
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
                        Debug.Log(triggered.currentInterObj.name);
                    }
                    catch
                    {
                        Debug.Log("Can't help lol");
                    }  
                    break;

                case "enter":
                    if ((parts.Length == 1) && (triggered.currentInterObj.tag == "Entrance"))
                    {
                        var nextLocale = GameModel.currentLocale.getLocation(triggered.currentInterObj.name);
                        Debug.Log("Got go " + nextLocale);
                        if (nextLocale == null)
                        {
                            triggered.storyHead.text = "Sorry can't go to " + nextLocale + " | " + GameModel.currentLocale.Name;
                        }
                        else
                        {
                            triggered.LoadLocation();
                        }
                        triggered.failsafe = true;
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
                            if (triggered.currentInterObj.name == "Chest")
                            {
                                string[] itemOptions = { "CoinStatic", "KeyStatic", "RuneStatic" };
                                var rand = new System.Random();
                                var randomint = rand.Next(itemOptions.Length);
                                var randomItem = GameObject.Find(itemOptions[randomint]);
                                var randomItemText = GameObject.Find(itemOptions[randomint]).GetComponent<Text>().text;
                                triggered.inventory.AddItem(randomItem);
                                if (triggered.inventory.itemAdded != true)
                                {
                                    Debug.Log("Not enough space!");
                                    strResult = "You dont have enough space for " + randomItemText + "!";
                                }
                                else
                                {
                                    Debug.Log(randomItemText + " added to Inventory");
                                    strResult = (randomItemText + " added to Inventory");
                                    storyNarrative.GetComponent<Text>().text = "Nice!";
                                    anim.SetBool("Looted", true);
                                    triggered.currentInterObj.GetComponent<Collider2D>().enabled = false;
                                    triggered.currentInterObj = null;
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
                                    if (triggered.currentInterObj.name == "Owlett_Monster")
                                    {
                                        try
                                        {
                                            triggered.inventory.AddItem(GameObject.Find("Sword"));
                                            if (triggered.inventory.itemAdded != true)
                                            {
                                                Debug.Log("Not enough space!");
                                                strResult = "You dont have enough space for the Sword!";
                                            }
                                            else
                                            {
                                                Debug.Log("Got take Sword");
                                                strResult = "Taken the sword!";
                                                storyNarrative.GetComponent<Text>().text = "All power to you";
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

                                    if (triggered.currentInterObj.name == "Owlett_Monster")
                                    {
                                        try
                                        {
                                            triggered.inventory.AddItem(GameObject.Find("Gold"));
                                            if (triggered.inventory.itemAdded != true)
                                            {
                                                Debug.Log("Not enough space!");
                                                strResult = "You dont have enough space for the Gold!";
                                            }
                                            else
                                            {
                                                Debug.Log("Got take Gold");
                                                strResult = "Taken the Gold!";
                                                storyNarrative.GetComponent<Text>().text = "A wise choice.";
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
                        if ((triggered.currentInterObj.name == "Owlett_Monster") && (GameObject.Find("Sword").activeSelf) || (GameObject.Find("Gold").activeSelf))
                        {
                            try
                            {
                                var rand = new System.Random();
                                var randomBool = rand.Next(2) == 1;
                                if (!randomBool)
                                {
                                    triggered.inventory.AddItem(GameObject.Find("Sword"));
                                    if (triggered.inventory.itemAdded != true)
                                    {
                                        Debug.Log("Not enough space!");
                                        strResult = "You dont have enough space for the Sword!";
                                    }
                                    else
                                    {
                                        GameObject.Find("Sword").SetActive(false);
                                    }

                                    triggered.inventory.AddItem(GameObject.Find("Gold"));
                                    if (triggered.inventory.itemAdded != true)
                                    {
                                        Debug.Log("Not enough space!");
                                        strResult = "You dont have enough space for the Gold!";
                                    }
                                    else
                                    {
                                        GameObject.Find("Gold").SetActive(false);
                                    }

                                    Debug.Log("Got steal");
                                    strResult = "Stolen both items!";
                                    storyNarrative.GetComponent<Text>().text = "!!!";
                                    triggered.currentInterObj.SetActive(false);
                                    triggered.currentInterObj = null;
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
                                    triggered.currentInterObj.GetComponent<Text>().Equals("");
                                    triggered.currentInterObj.SetActive(false);
                                    triggered.currentInterObj = null;
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


