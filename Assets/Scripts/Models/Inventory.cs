using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public GameObject[] inventory = new GameObject[10];
    public bool itemAdded = false;

    public void AddItem(GameObject item)
    {
        // Find first open slot in inventory
        for (int i = 0; i < GameModel.inventory.Length; i++)
        {
            if (GameModel.inventory[i] == null)
            {
                GameModel.inventory[i] = item;
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
    
    
}
