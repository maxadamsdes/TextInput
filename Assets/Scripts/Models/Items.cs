using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class Items
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int LocationID { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public bool Picked { get; set; }


    public void addItem(string pPlayerName, int pLocationID, string pItemName, string pIcon, float pPositionX, float pPositionY)
    {
        Items newItem = GameModel.ds.storeNewPlayerItems(pPlayerName, pLocationID, pItemName, pIcon, pPositionX, pPositionY);
        
    }

}
