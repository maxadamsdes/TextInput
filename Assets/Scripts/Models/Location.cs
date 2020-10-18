using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
public class Location 
{
    
    private int id;
    private string name;
    private string story;
    private Vector3 nEntry;
    private Vector3 sEntry;
    private Vector3 eEntry;
    private Vector3 wEntry;
    [PrimaryKey, AutoIncrement]
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }
    public Vector3 NEntry { get => nEntry; set => nEntry = value; }
    public Vector3 SEntry { get => sEntry; set => sEntry = value; }
    public Vector3 EEntry { get => eEntry; set => eEntry = value; }
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

        Location newLocation = GameModel.ds.storeNewLocation(pName, pStory, pNEntry, pSEntry, pEEntry, pWEntry);
        addDirection(pDirection, newLocation);

        //Location newLocation = new Location
        //{
        //    Name = pName,
        //    Story = pStory,
        //    NEntry = pNEntry,
        //    SEntry = pSEntry,
        //    EEntry = pEEntry,
        //    WEntry = pWEntry
        //};

        //Locations.Add(pDirection, newLocation);

    }
    public void addDirection(string pDirection, Location toLocation)
    {
        GameModel.ds.storeFromTo(Id, toLocation.Id, pDirection);
    }

    public  void addLocation(string pDirection, Location pLocation)
    {
        addDirection(pDirection, pLocation);

        // Locations.Add(pDirection, pLocation);
    }

    public Location getLocation(string pDirection)
    {
        Location thisLocation = null;
        
        //if(! Locations.TryGetValue(pDirection, out thisLocation)) {
        //    Debug.Log(" Bad location");
        //}

        ToFrom tf = GameModel.ds.GetToFrom(Id, pDirection);
        if (tf != null)
            thisLocation = GameModel.ds.GetLocation(tf.ToID);

        return thisLocation;
    }
}
