using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string locationName;
    public int health;
    public float[] position;
    public Location location;
    public PlayerData (Player player)
    {
        location = player.Location;
        locationName = player.LocationName;
        health = player.Health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
