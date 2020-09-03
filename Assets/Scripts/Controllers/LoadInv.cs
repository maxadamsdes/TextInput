using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadInv : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        //GetComponent<Text>().text = "Item 1 " + GameModel.inventory[0].name;
        GameModel.LoadInventoryItems();
    }
}
