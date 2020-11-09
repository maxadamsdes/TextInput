using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int locationId;
    public int health;
    public float[] position;
    public int score;
    public PlayerData (Player player)
    {
        locationId = player.LocationId;
        //health = player.Health;

        position = new float[3];
        position[0] = GameModel.currentPlayer.transform.position.x;
        position[1] = GameModel.currentPlayer.transform.position.y;
        position[2] = GameModel.currentPlayer.transform.position.z;

    }
}
