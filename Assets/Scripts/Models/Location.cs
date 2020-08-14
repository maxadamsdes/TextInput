using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location 
{
    private string name;
    private string story;

    // what about location assests??

    private Dictionary<string, Location> Locations = new Dictionary<string, Location>();

    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }

    public void addLocation(string pDirection ,string pName, string pStory)
    {
        Location newLocation = new Location
        {
            Name = pName,
            Story = pStory
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
