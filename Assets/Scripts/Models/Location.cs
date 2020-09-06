using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location 
{
    private string name;
    private string story;
    
    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }
    
    public Vector3 NEntry { get => nEntry; set => nEntry = value; }
    private Vector3 sEntry;
    public Vector3 SEntry { get => sEntry; set => sEntry = value; }
    private Vector3 eEntry;
    public Vector3 EEntry { get => eEntry; set => eEntry = value; }
    private Vector3 nEntry;
    private Vector3 wEntry;
    public Vector3 WEntry { get => wEntry; set => wEntry = value; }
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

    

    public void addLocation(string pDirection ,string pName, string pStory, Vector3 pNEntry, Vector3 pSEntry, Vector3 pEEntry, Vector3 pWEntry)
    {
        Location newLocation = new Location
        {
            Name = pName,
            Story = pStory,
            NEntry = pNEntry,
            SEntry = pSEntry,
            EEntry = pEEntry,
            WEntry = pWEntry
        };

        Locations.Add(pDirection, newLocation);

    }

    public  void addLocation(string pDirection, Location pLocation)
    {
        Locations.Add(pDirection, pLocation);
    }

    public Location getLocation(string pDirection)
    {
        Location thisLocation = null;
        
        if(! Locations.TryGetValue(pDirection, out thisLocation)) {
            Debug.Log(" Bad location");
        }

        return thisLocation;
    }
}
