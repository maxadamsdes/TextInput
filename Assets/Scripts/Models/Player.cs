using UnityEngine;
using System.Collections;

public class Player 
{

    private string name;
    private Location location;
    private int health;
    private int wealth;
    // what about inventory?

    public string Name { get => name; set => name = value; }
    public Location Location { get => location; set => location = value; }
    public int Health { get => health; set => health = value; }
    public int Wealth { get => wealth; set => wealth = value; }
}
