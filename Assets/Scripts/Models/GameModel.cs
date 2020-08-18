using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;



using System.Text;

// Is this a factory?

public static class GameModel
{

	static String _name;

	public static string Name{
		get 
		{ 
			return _name;  
		}
		set{
			_name = value; 
		}

	}

    static String _event;
    public static string Event
    {
        get
        {
            return _event;
        }
        set
        {
            _event = value;
        }
    }

    public static Location currentLocale;
    public static Player currentPlayer;
    public static Event currentEvent;

    public static void MakeGame()
    {
        Location forest, cave, beach, river, highway;
        currentLocale = new Location
        {
            Name = "Forest",
            Story = " Run!!"
        };



        // forest
        forest = currentLocale;
        forest.addLocation("North", "Cave", "Lava");
        forest.addLocation("East", "Beach", "Sharks");
        forest.addLocation("West", "Crossroads", "Highwaymen!");

        // castle
        cave = forest.getLocation("North");
        cave.addLocation("South", forest);
        cave.addLocation("East", "River", "Pebbles");
        river = cave.getLocation("East");

        // beach
        beach = forest.getLocation("East");
        beach.addLocation("West", forest);
        beach.addLocation("North", river);

        //river
        river.addLocation("West", cave);
        river.addLocation("South", beach);

        //Highway
        highway = forest.getLocation("West");
        highway.addLocation("East", forest);
    }


}

