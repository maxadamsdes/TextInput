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
        Location forest, cave, cave2, beach, river, highway, ocean;
        currentLocale = new Location
        {
            Name = "Forest",
            Story = " Run!!"
        };



        // forest
        forest = currentLocale;
        forest.addLocation("North", "Cave", "Lava");
        forest.addLocation("East", "Beach", "Sharks");
        forest.addLocation("West", "Highway", "Highwaymen!");

        // cave
        cave = forest.getLocation("North");
        cave.addLocation("South", forest);
        cave.addLocation("East", "Cave2", "Enemies?");

        // cave 2
        cave2 = cave.getLocation("East");
        cave2.addLocation("West", cave);
        cave2.addLocation("East", "River", "Bridge");
        river = cave2.getLocation("East");

        // beach
        beach = forest.getLocation("East");
        beach.addLocation("West", forest);
        beach.addLocation("East", "Ocean", "I guess you're amphibious");
        ocean = beach.getLocation("East");

        // river
        river.addLocation("West", cave2);
        river.addLocation("South", ocean);

        // ocean
        ocean.addLocation("North", river);
        ocean.addLocation("West", beach);

        //Highway
        highway = forest.getLocation("West");
        highway.addLocation("East", forest);
    }


}

