using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
public class Location 
{

    private int id;
    private string name;
    private string story;
    private float nEntryX;
    private float sEntryX;
    private float eEntryX;
    private float wEntryX;
    private float nEntryY;
    private float sEntryY;
    private float eEntryY;
    private float wEntryY;
    private string item1Name;
    private float item1PositionX;
    private float item1PositionY;
    //private Sprite item1Icon;
    private string item2Name;
    private float item2PositionX;
    private float item2PositionY;
    //private Sprite item2Icon;
    private string item3Name;
    private float item3PositionX;
    private float item3PositionY;
    //private Sprite item3Icon;

    [PrimaryKey, AutoIncrement]
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }
    public float NEntryX { get => nEntryX; set => nEntryX = value; }
    public float SEntryX { get => sEntryX; set => sEntryX = value; }
    public float EEntryX { get => eEntryX; set => eEntryX = value; }
    public float WEntryX { get => wEntryX; set => wEntryX = value; }
    public float NEntryY { get => nEntryY; set => nEntryY = value; }
    public float SEntryY { get => sEntryY; set => sEntryY = value; }
    public float EEntryY { get => eEntryY; set => eEntryY = value; }
    public float WEntryY { get => wEntryY; set => wEntryY = value; }
    public string Item1Name { get => item1Name; set => item1Name = value; }
    public float Item1PositionX { get => item1PositionX; set => item1PositionX = value; }
    public float Item1PositionY { get => item1PositionY; set => item1PositionY = value; }
    //public Sprite Item1Icon { get => item1Icon; set => item1Icon = value; }
    public string Item2Name { get => item2Name; set => item2Name = value; }
    public float Item2PositionX { get => item2PositionX; set => item2PositionX = value; }
    public float Item2PositionY { get => item2PositionY; set => item2PositionY = value; }
    //public Sprite Item2Icon { get => item2Icon; set => item2Icon = value; }
    public string Item3Name { get => item3Name; set => item3Name = value; }
    public float Item3PositionX { get => item3PositionX; set => item3PositionX = value; }
    public float Item3PositionY { get => item3PositionY; set => item3PositionY = value; }
    //public Sprite Item3Icon { get => item3Icon; set => item3Icon = value; }

    public void addLocation(string pDirection ,string pName, string pStory, float pNEntryX, float pNEntryY, float pSEntryX, float pSEntryY, float pWEntryX, float pWEntryY, float pEEntryX, float pEEntryY)
    {

        Location newLocation = GameModel.ds.storeNewLocation(pName, pStory, pNEntryX, pNEntryY, pSEntryX, pSEntryY, pWEntryX, pWEntryY, pEEntryX, pEEntryY);
        addDirection(pDirection, newLocation);


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

    public void AddItem(Location pLocationName, string pItemName, float pPositionX, float pPositionY)
    {
        GameModel.ds.AddItem(pLocationName, pItemName, pPositionX, pPositionY);
    }

    public void RemoveItem(Location pLocationName, string pItemName, float pPositionX)
    {
        GameModel.ds.RemoveItem(pLocationName, pItemName, pPositionX);
    }

    //public Vector3 GetItem(string pName)
    //{
    //    return GameModel.items(pName);
    //    return Items[pName];
    //}
}
