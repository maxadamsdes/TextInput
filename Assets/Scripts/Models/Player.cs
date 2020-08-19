using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private string name;
    private Location location;
    private string locationName;
    private int health;
    private int wealth;
    // what about inventory?

    public string Name { get => name; set => name = value; }
    public Location Location { get => location; set => location = value; }
    public string LocationName { get => locationName; set => locationName = value; }
    public int Health { get => health; set => health = value; }
    public int Wealth { get => wealth; set => wealth = value; }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        location = data.location;
        locationName = data.locationName;
        health = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

}
