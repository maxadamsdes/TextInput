using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInv : MonoBehaviour
{
    void Start()
    {
        GameModel.LoadInventoryItems();
    }
}
