using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using System;

[Serializable]
public class Location
{

    private int id;
    private string name;
    private string story;

    [PrimaryKey]
    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }

    public void addLocation(string pDirection, string pName, string pStory, float pNEntryX, float pNEntryY, float pSEntryX, float pSEntryY, float pWEntryX, float pWEntryY, float pEEntryX, float pEEntryY)
    {
        Location newLocation = GameModel.ds.storeNewLocation(pName, pStory);
        addDirection(pDirection, newLocation);
    }
    public void addDirection(string pDirection, Location toLocation)
    {
        GameModel.ds.storeFromTo(Name, toLocation.Name, pDirection);
    }

    public void addLocation(string pDirection, Location pLocation)
    {
        addDirection(pDirection, pLocation);
    }

    public Location getLocation(string pDirection)
    {
        Location thisLocation = null;

        ToFrom tf = GameModel.ds.GetToFrom(Name, pDirection);
        if (tf != null)
            thisLocation = GameModel.ds.GetLocation(tf.ToID);

        return thisLocation;
    }
}
