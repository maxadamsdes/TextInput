using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class CommandProcessor
{
    MenuController menuController = new MenuController();
    
    public CommandProcessor ()
		{
		}
        
        
		public String Parse(String pCmdStr){
        TriggerEvent triggered;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        triggered = playerObj.GetComponent<TriggerEvent>();
        Text storyText;
        GameObject outputField;
        outputField = GameObject.FindGameObjectWithTag("Narrative");
        storyText = outputField.GetComponent<Text>();

        String strResult = "Do not understand";;
			pCmdStr = pCmdStr.ToLower();
			String[] parts = pCmdStr.Split(' '); // tokenise the command
            Location nextLocale;

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
                                    storyText.text = Convert.ToString(GameModel.currentEvent);
                                }
                                else
                                {
                                    Debug.Log(triggered.currentInterObj.name + " added to Inventory");
                                    strResult = triggered.currentInterObj.name + " added to Inventory";
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
                                nextLocale = GameModel.currentLocale.getLocation("North");
                                if (nextLocale == null)
                                    strResult = "Sorry can't go North " + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }

                                break;
                            case "south":
                                Debug.Log("Got go South");
                                nextLocale = GameModel.currentLocale.getLocation("South");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go South " + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                            break;
                            case "east":
                                Debug.Log("Got go East");
                                nextLocale = GameModel.currentLocale.getLocation("East");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go East " + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                            break;
                        case "west":
                            Debug.Log("Got go West");
                            nextLocale = GameModel.currentLocale.getLocation("West");
                            if (nextLocale == null)
                            {
                                strResult = "Sorry can't go West " + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            else
                            {
                                GameModel.currentLocale = nextLocale;
                                strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            break;
                        default:
                                Debug.Log(" do not know how to go there");
                                strResult = "Do not know how to go there";
                                break;
                        }// end switch
                        break;
                    case "take":
                        switch (parts[1])
                        {
                            case "sword":
                                Debug.Log("Got take Sword");
                                strResult = "Taken the sword!";

                                break;
                            case "gold":
                                Debug.Log("Got take Gold");
                                strResult = "Taken the gold!";
                                break;
                            default:
                                Debug.Log(" take what?");
                                strResult = "Can only take the sword or the gold!";
                                break;
                        }
                    break;
                case "steal":
                    var rand = new System.Random();
                    var randomBool = rand.Next(2) == 1;
                    if (!randomBool)
                    {
                        Debug.Log("Got steal");
                        strResult = "Stolen both items!";

                        if (parts.Length == 3)
                        {
                            String param = parts[2];
                        }// do pick up command
                         // GameModel.Pickup();
                    }
                    else
                    {
                        Debug.Log("Failed steal");
                        strResult = "You were caught!";
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


