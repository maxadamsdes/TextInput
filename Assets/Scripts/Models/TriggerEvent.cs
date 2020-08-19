using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TriggerEvent : MonoBehaviour
{
    public GameObject inputManager;
    public GameObject currentInterObj = null;
    
    public GameObject playerObj;
    public Text storyHead;
    public Text storyNarrative;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;
    public Animator anim;
    public Location nextLocale;
    public GameObject parentLocation;
    public Transform[] locations;
    bool failsafe = false;
    string locationStr;
    Collider2D Destination;

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("GameController");
        storyHead.text = GameModel.currentLocale.Name;
        locations = parentLocation.GetComponentsInChildren<Transform>(true);
    }

    void Update()
    {
        if (Destination)
        {
            if ((!failsafe) && (Destination.tag == "Entrance") && (Input.GetKeyDown("e")))
            {
                Debug.Log("Got go " + Destination.name);
                nextLocale = GameModel.currentLocale.getLocation(Destination.name);
                if (nextLocale == null)
                {
                    storyHead.text = "Sorry can't go " + Destination.name + " | " + GameModel.currentLocale.Name;
                }
                else
                {
                    LoadLocation();
                }
                failsafe = true;

            }
        }

    }

    void PickUpItem()
    {
        if (currentInterObj)
        {
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
                currentInterObj.SendMessage("Do Interaction");
            }
        }
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D encounter)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(encounter, gameObject, OnTriggerExit2D);
        if (encounter.name == "Trap")
        {
            Debug.Log("Got go South");
            nextLocale = GameModel.currentLocale.getLocation("South");
            if (nextLocale == null)
            {
                storyHead.text = "Sorry can't go South | " + GameModel.currentLocale.Name;
            }
            else
            {
                LoadLocation();
            }
            failsafe = true;
        }
        else if ((!failsafe) && (encounter.tag == "Entrance"))
        {
            Destination = encounter;
            storyHead.text = "Press E to enter " + GameModel.currentLocale.getLocation(encounter.name).Name;
        }
        else if (encounter.tag == "Respawn")
        {
            if (encounter.name == "RespawnerCave")
            {
                Vector3 temp = new Vector3(2.4f, 1.47f, 0);
                playerObj.transform.position = temp;
                playerObj.GetComponent<PlayerMovement>().horizontalMove = 0f;
            }
            else if (encounter.name == "RespawnerCave2")
            {
                Vector3 temp = new Vector3(3.06f, 1.59f, 0);
                playerObj.transform.position = temp;
                playerObj.GetComponent<PlayerMovement>().horizontalMove = 0f;
            }


        }
        else if (encounter.tag == "Sign")
        {
            currentInterObj = encounter.gameObject;
            storyHead.text = currentInterObj.tag;
            storyNarrative.text = currentInterObj.GetComponent<Text>().text;
        }
        else if (encounter.tag != "Entrance")
        {

            currentInterObj = encounter.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
            storyHead.text = currentInterObj.tag;
            storyNarrative.text = currentInterObj.GetComponent<Text>().text;
            if (encounter.name == "Chest")
            {
                anim = encounter.GetComponent<Animator>();
                anim.SetBool("Encountered", true);
            }
            inputManager.GetComponent<MenuController>().openText();
            failsafe = true;
        }
    }

    void OnTriggerExit2D(Collider2D encounter)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(encounter, gameObject);
        if (encounter.name == "Chest")
        {
            anim = encounter.GetComponent<Animator>();
            anim.SetBool("Encountered", false);
        }
        else if (encounter.tag == "Entrance")
        {
            Destination = null;
            storyHead.text = GameModel.currentLocale.Name;
        }
        else
        {
            storyHead.text = GameModel.currentLocale.Name;
            storyNarrative.text = "";
        }
        failsafe = false;
        
    }

    public void LoadLocation()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].name == nextLocale.Name)
            {
                locations[i].gameObject.SetActive(true);
                GameObject.Find(GameModel.currentLocale.Name).SetActive(false);
                GameModel.currentLocale = nextLocale;
                playerObj.transform.position = new Vector3(0f, -1.47f, 0);
                storyHead.text = nextLocale.Name;
                storyNarrative.text = nextLocale.Story;
                break;
            }
        }
    }
 
}
