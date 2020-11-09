using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

[Serializable]
public class Player
{
    
    private string playerName;
    private string password;
    private int locationId;
    //private int health;
    private int score;
    [PrimaryKey]
    public string PlayerName { get => playerName; set => playerName = value; }
    public string Password { get => password; set => password = value; }
    public int LocationId { get => locationId; set => locationId = value; }
    //public int Health { get => health; set => health = value; }
    public int Score { get => score; set => score = value; }
   


    //public Inventory Inventory { get => inventory; set => inventory = value; }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        locationId = data.locationId;
        score = data.score;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        GameModel.currentPlayer.transform.position = position;
    }

}
