//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public static class Inventory
//{
//    public GameObject[] inventory = new GameObject[10];
//    public static bool itemAdded = false;
//    private static GameObject invItemImage;
//    private static GameObject invItemName;


//    public static void AddItem(GameObject item)
//    {
//        Find first open slot in inventory
//        for (int i = 0; i < GameModel.inventory.Length; i++)
//        {
//            if (GameModel.inventory[i] == null)
//            {
//                GameModel.inventory[i] = item;
//                Debug.Log(item.name + " was added!");

//                itemAdded = true;
//                break;
//            }

//            If inventory was full
//            if (!itemAdded)
//            {
//                Debug.Log("Inventory Full - Item not Added");
//            }

//        }
//    }

//    public static void LoadInventoryItems()
//    {
//        for (int i = 0; i < GameModel.inventory.Length; i++)
//        {
//            if (GameModel.inventory[i] != null)
//            {
//                invItemImage = GameObject.Find("ItemImage" + Convert.ToString(i + 1));
//                invItemImage.GetComponent<Image>().sprite = GameModel.inventory[i].GetComponent<Image>().sprite;
//                invItemName = GameObject.Find("ItemText" + Convert.ToString(i + 1));
//                invItemName.GetComponent<Text>().text = GameModel.inventory[i].GetComponent<Text>().text;
//                invItemImage = GameObject.Find("ItemImage1");
//                invItemImage.GetComponent<Image>().sprite = GameModel.inventory[0].GetComponent<Image>().sprite;
//                invItemName = GameObject.Find("ItemText1");
//                invItemName.GetComponent<Text>().text = GameModel.inventory[0].GetComponent<Text>().text;
//            }
//            else
//            {
//                break;
//            }
//        }
//    }

//}
