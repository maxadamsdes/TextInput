using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

public class LocationItems
{
    // what about location assets??
    private Dictionary<string, Vector3> items = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> Items { get => items; set => items = value; }

    private Dictionary<string, Location> Locations = new Dictionary<string, Location>();

    public void AddItem(string pName, Vector3 pPosition)
    {
        Items.Add(pName, pPosition);
    }

    public void RemoveItem(string pName)
    {
        Items.Remove(pName);
    }

    public Vector3 GetItem(string pName)
    {
        return Items[pName];
    }
}
