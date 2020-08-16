using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject inputManager;
    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("GameController");
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
        currentInterObj = encounter.gameObject;
        currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
         
        inputManager.GetComponent<MenuController>().openText();

        //if (encounter.gameObject.tag == "Player")
        //{
        //    inputManager.GetComponent<MenuController>().openText();
        //}
    }

}
