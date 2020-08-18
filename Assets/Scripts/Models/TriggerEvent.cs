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
    Location nextLocale;
    bool failsafe = false;

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("GameController");
        storyHead.text = GameModel.currentLocale.Name;
        
    }

    void Update()
    {
        
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
        if ((!failsafe) && (encounter.tag == "Entrance"))
        {
            Debug.Log("Got go " + encounter.name);
            nextLocale = GameModel.currentLocale.getLocation(encounter.name);
            if (nextLocale == null)
            {
                storyHead.text = "Sorry can't go " + encounter.name + " | " + GameModel.currentLocale.Name;
            }
            else
            {
                GameModel.currentLocale = nextLocale;
                storyHead.text = GameModel.currentLocale.Name;
                storyNarrative.text = GameModel.currentLocale.Story;
                SceneManager.LoadScene(nextLocale.Name);
            }

        }
        else if (encounter.tag == "Respawn")
        {
            Vector3 temp = new Vector3(2.4f, 1.47f, 0);
            playerObj.transform.position = temp;
            playerObj.GetComponent<PlayerMovement>().horizontalMove = 0f;

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
        }
        failsafe = true;



        //if (encounter.gameObject.tag == "Player")
        //{
        //    inputManager.GetComponent<MenuController>().openText();
        //}
    }

    void OnTriggerExit2D(Collider2D encounter)
    {
        if (encounter.name == "Chest")
        {
            anim = encounter.GetComponent<Animator>();
            anim.SetBool("Encountered", false);
        }
        failsafe = false;
        
    }

    private void SetStory()
    {
        

    }
}
