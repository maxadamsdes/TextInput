﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class Items
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public string LocationID { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string PositionX { get; set; }
    public string PositionY { get; set; }
    public int Picked { get; set; }


    public void addItem(string pPlayerName, string pLocationID, string pItemName, string pIcon, float pPositionX, float pPositionY, int pPicked)
    {
        Items newItem = GameModel.ds.storeNewPlayerItems(pPlayerName, pLocationID, pItemName, pIcon, pPositionX, pPositionY, pPicked);
        
    }

}
