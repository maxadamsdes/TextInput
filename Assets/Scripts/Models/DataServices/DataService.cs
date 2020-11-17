using SQLite4Unity3d;
using UnityEngine;
using System.Linq;
// DataService is a bridge to SQlite 
// =================================
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;
    public SQLiteConnection Connection { get { return _connection; } }
    public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

    // Drops previous databse table locations to then update it with the data from the server
    public void SaveLocations(List<Location> Locations)
    {
        _connection.DropTable<Location>();
        _connection.CreateTable<Location>();
        _connection.InsertAll(Locations);
    }

    public List<Location> LoadLocations()
    {
        return _connection.Table<Location>().ToList<Location>();
    }

    public bool HaveLocations()
    {
        try {
            return LoadLocations().Count > 0;
        }
        catch
        {
            return false;
        }

    }


    /* This drops all local and  */
	public void CreateDB(){
        // remove these once testing is sorted. These remove their respective tables from the databases.
        //_connection.DropTable<Location>(); 
        //_connection.DropTable<ToFrom>();
        //_connection.DropTable<Player>();
        //_connection.DropTable<Items>();
        GameModel.jsDrop.Drop<Location, JsnReceiver>(jsnReceiverDel);
        GameModel.jsDrop.Drop<Player, JsnReceiver>(jsnReceiverDel);
        GameModel.jsDrop.Drop<Items, JsnReceiver>(jsnReceiverDel);

        // creating the schema
        _connection.CreateTable<Location>();
        _connection.CreateTable<ToFrom>();
        _connection.CreateTable<Player>();
        _connection.CreateTable<Items>();
        GameModel.jsDrop.Create<Player, JsnReceiver>(new Player
        {
            PlayerName = "UUUUUUUUUUUUUUUUUUUUUUUUU",
            Score = 0,
            LocationId = "UUUUUUUUUUUUUUUUUUUUUUUUU",
            Password = "***************************"
        }, jsnReceiverDel);

        GameModel.jsDrop.Create<Location, JsnReceiver>(new Location
        {
            Name = "UUUUUUUUUUUUUUUUUUUU",
            Story = "UUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU"
        }, jsnReceiverDel);

        GameModel.jsDrop.Create<Items, JsnReceiver>(new Items
        {
            PlayerName = "UUUUUUUUUUUUUUUUUUUUUUUUU",
            Name = "UUUUUUUUUUUUUUUU",
            Icon = "UUUUUUUUUUUUUUUU",
            LocationID = "UUUUUUUUUUUUUUUUUUUUUU",
            PositionX = "UUUUUUUUUUUUUUUUUUUUUU",
            PositionY = "UUUUUUUUUUUUUUUUUUUUUU",
            Picked = 0
        }, jsnReceiverDel);
    }

    public void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
        // To do: parse and produce an appropriate response
    }

    public void jsnReceiverItemsDel(JsnReceiver pReceived)
    {
        Debug.Log("Yeah it didn't work for items chief");
        // To do: parse and produce an appropriate response
    }

    public void jsnStorePlayer(List<Player> pReceivedList)
    {
        GameModel.jsDrop.Store<Player, JsnReceiver>(pReceivedList, jsnReceiverDel);
    }

    public void jsnGetPlayer(List<Player> pReceivedList)
    {
        GameModel.jsDrop.Select<Player, JsnReceiver>("PlayerName = '" + pReceivedList[0].PlayerName + "'", jsnPlayerListReceiverDel, jsnReceiverDel);
    }
    public void jsnGetAllPlayersScores()
    {
        GameModel.jsDrop.Select<Player, JsnReceiver>("Score >= 5", GameModel.populateScoreBoard.jsnPlayerHighScoreListReceiverDel, jsnReceiverDel);
    }

    public void jsnGetAllPlayers()
    {
        GameModel.jsDrop.Select<Player, JsnReceiver>("Score >= 0", jsnPlayersListReceiverDel, jsnReceiverDel);
    }
    public void jsnPlayerHighScoreListReceiverDel(List<Player> pReceivedList)
    {
        // Gives indication as to whether the update has worked
        Debug.Log("Received items " + pReceivedList.Count);
        foreach (Player lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.PlayerName + "," + lcReceived.Password + "," + lcReceived.Score.ToString() + "," + lcReceived.LocationId.ToString() + "}");
        }
        
    }
    public void jsnPlayersListReceiverDel(List<Player> pReceivedList)
    {
        // Gives indication as to whether the update has worked
        Debug.Log("Received items " + pReceivedList.Count);
        _connection.DropTable<Player>();
        _connection.CreateTable<Player>();
        _connection.InsertAll(pReceivedList);
    }
    public void jsnPlayerListReceiverDel(List<Player> pReceivedList)
    {
        // Gives indication as to whether the update has worked
        Debug.Log("Received items " + pReceivedList.Count);
        foreach (Player lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.PlayerName + "," + lcReceived.Password + "," + lcReceived.Score.ToString() + "," + lcReceived.LocationId.ToString() + "}");
        }
    }

    public void jsnGetPlayerLocations(List<int> pReceivedLocations)
    {
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 0 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 1 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 2 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 3 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 4 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 5 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
        GameModel.jsDrop.Select<Player, JsnReceiver>("LocationId = '" + 6 + "'", GameModel.gameMap.jsnPlayerLocationListReceiverDel, jsnReceiverDel);
    }

    public void jsnLocationListReceiverDel(List<Location> pReceivedList)
        {
            _connection.DeleteAll<Location>();
            _connection.InsertAll(pReceivedList);
        }

    public void jsnGetPlayerLocationItems(List<Player> pReceivedList)
    {
        GameModel.jsDrop.Select<Items, JsnReceiver>("PlayerName = '" + pReceivedList[0].PlayerName + "'", jsnPlayerLocationItemsReceiverDel, jsnReceiverItemsDel);   
    }


    public void jsnPlayerLocationItemsReceiverDel(List<Items> pReceivedList)
    {
        Debug.Log("Received items " + pReceivedList.Count);
        foreach (Items lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.PlayerName + "," + lcReceived.LocationID.ToString() + "," + lcReceived.Name + "," + lcReceived.PositionX + "," + lcReceived.Picked + "}");
        }
        _connection.InsertAll(pReceivedList);
    }

    // Updates the servers database when an item is changed
    public void jsnUpdateItems(List<Items> pReceivedList)
    {
        // Wipes the old data from player record (acts as a failsafe for store)
        GameModel.jsDrop.Delete<Items, JsnReceiver>("PlayerName = '" + pReceivedList[0].PlayerName + "'", jsnReceiverDel);
        // Stores the altered item in the servers db
        GameModel.jsDrop.Store<Items, JsnReceiver>(pReceivedList, jsnReceiverDel);
    }


    // Locations and their relationships 
    public IEnumerable<Location> GetLocations()
    {
        return _connection.Table<Location>();
    }

    public List<Items> GetLocationItems(Player pPlayer)
    {
        return _connection.Table<Items>().Where(i => i.LocationID == pPlayer.LocationId && i.Picked == 0).ToList<Items>();
    }

    public List<Items> GetAllLocationItems(Player pPlayer)
    {
        return _connection.Table<Items>().ToList<Items>();
    }

    public bool haveLocations()
    {
        return _connection.Table<Location>().Count() > 0;
    }

    public ToFrom GetToFrom(string pFromID, string pDirection)
    {
        return _connection.Table<ToFrom>().Where(tf => tf.FromID == pFromID
                                                      && tf.Direction == pDirection).FirstOrDefault();      
    }

    public Location GetLocation(string pLocationName)
    {
        return _connection.Table<Location>().Where(l => l.Name == pLocationName).FirstOrDefault();
    }

    public Location GetPlayerLocation (Player aPlayer)
    {
        return GetLocation(aPlayer.LocationId);
    }

    public void updatePlayer(Player pPlayer)
    {
        _connection.InsertOrReplace(pPlayer);
        List<Player> PlayerList = new List<Player>();
        PlayerList.Add(GameModel.cPlayer);
        jsnStorePlayer(PlayerList);

    }

    public Location getGameExist()
    {
        return _connection.Table<Location>().Where(l => l.Name == "Forest").FirstOrDefault();
    }

    public Location storeNewLocation(string pName, string pStory)
    {
        Location newLocation = new Location
        {
            Name = pName,
            Story = pStory
        };
        _connection.Insert(newLocation); // Store the location 
        //GameModel.currentLocale = newLocation;
        return newLocation;  // return the location
    }
    public Location storeLocation(Location pLocation)
    {
        _connection.InsertOrReplace(pLocation); // Store the location 
        return pLocation; 
    }


    public void storeFromTo(string pFromID, string pToID, string pDirection)
    {
        ToFrom f = new ToFrom
        {
            ToID = pToID,
            FromID = pFromID,
            Direction = pDirection
        };
        _connection.Insert(f);

    }


    // Player
    public Player storeNewPlayer(string pName, string pPassword, string pLocationId, int pScore)
    {
        Player player = new Player
        {
            PlayerName = pName,
            Password = pPassword,
            LocationId = pLocationId,
            Score = pScore

        };
        _connection.Insert(player);
        return player;
    }

    public Player getPlayer(string pPlayerName)
    {
        return _connection.Table<Player>().Where(x => x.PlayerName == pPlayerName).FirstOrDefault();
    }

    public void AddPlayerItem(Player pPlayer, Location pLocation, string pItemName)
    {
        Items newItem = new Items();
        newItem.PlayerName = pPlayer.PlayerName;
        newItem.LocationID = pLocation.Name;
        newItem.Name = pItemName;
        _connection.Insert(newItem);
    }

    public void AddPlayerItems(Player pPlayer)
    {
        _connection.DropTable<Items>();
        _connection.CreateTable<Items>();
        // Creates the first new Item
        Items item = new Items
        {
            PlayerName = pPlayer.PlayerName,
            LocationID = "Forest",
            Name = "Coin",
            Icon = "Coin",
            PositionX = "-1",
            PositionY = "0",
            Picked = 0
        };

        // Where all other items are added by specifying their location, name, sprite and position
        item.addItem(pPlayer.PlayerName, "Forest", "Rune", "Rune", -2.456395f, -1.7982f, 0);
        item.addItem(pPlayer.PlayerName, "Forest", "Sword", "Sword", 0f, -10f, 0);
        item.addItem(pPlayer.PlayerName, "Forest", "Gold", "Gold", 0f, -10f, 0);
        item.addItem(pPlayer.PlayerName, "Forest", "Owlett_Monster", "Owlett_Monster", 9.8286f, 0.4f, 0);
        item.addItem(pPlayer.PlayerName, "Cave", "Chest", "Chest", 11.868f, 0.2910001f, 0);
        item.addItem(pPlayer.PlayerName, "Cave", "Coin", "Coin", 8.63f, -2.57f, 0);
        item.addItem(pPlayer.PlayerName, "River", "Chest", "Chest", 23.31f, -1.529251f, 0);
        item.addItem(pPlayer.PlayerName, "House", "Key", "Key", 6.2f, 0.56f, 0);
        item.addItem(pPlayer.PlayerName, "Cave2", "Coin", "Coin", 15.86f, -0.18f, 0);
        item.addItem(pPlayer.PlayerName, "Beach", "Rune", "Rune", -4.9f, -1.79f, 0);
        item.addItem(pPlayer.PlayerName, "Ocean", "Chest", "Chest", 2.409f, -13.283f, 0);
        item.addItem(pPlayer.PlayerName, "Ocean", "Key", "Key", -11.72f, -15.68f, 0);
        item.addItem(pPlayer.PlayerName, "Forest", "Door", "Door", -3.54f, -1.17f, 0);

        List<Items> newItemList = GetAllLocationItems(pPlayer);
        jsnUpdateItems(newItemList);

    }

    // used for adding items to the database
    public Items storeNewPlayerItems(string pPlayerName, string pLocationID, string pItemName, string pIcon, float pPositionX, float pPositionY, int pPicked)
    {
        string positionX = pPositionX.ToString();
        string positionY = pPositionY.ToString();
        // Sets an item to match input
        Items newItem = new Items
        {
            PlayerName = pPlayerName,
            LocationID = pLocationID,
            Name = pItemName,
            Icon = pIcon,
            PositionX = positionX,
            PositionY = positionY,
            Picked = pPicked
        };
        // Stores new item into database
        _connection.Insert(newItem);
        
        // returns the item
        return newItem;
    }

    
    // Changes an items state to picked up
    public void Removeitem(string pPlayerName, string pLocationID, string pItemName, float pPositionX)
    {
        // creates a list that will be used for updating the server database later
        List<Items> itemList = new List<Items>();
        // Attempts to retrieve an item from the local DB using given attributes
        Items item = GetItem(pPlayerName, pLocationID, pItemName, pPositionX);
        _connection.Delete(item);
        // Changes the items state to be picked up
        if(pItemName == "Chest")
        {
            item.Picked = 2;
        }
        else
        {
            item.Picked = 1;
        }
        // Updates local db to match change
        _connection.InsertOrReplace(item);
        // Adds updated item to a list so that the jsnDrop is able to receive the data
        itemList.Add(item);
        jsnUpdateItems(itemList);
    }

    public void UseItem(Items pItem1, Items pItem2, Items pDoor)
    {
        _connection.Delete(pItem1);
        _connection.Delete(pItem2);
        _connection.Delete(pDoor);
        pItem1.Picked = 2;
        pItem2.Picked = 2;
        pDoor.Picked = 2;
        _connection.InsertOrReplace(pItem1);
        _connection.InsertOrReplace(pItem2);
        _connection.InsertOrReplace(pDoor);
        List<Items> itemList = new List<Items>();
        itemList = GetAllLocationItems(GameModel.cPlayer);
        jsnUpdateItems(itemList);
    }

    // Rertrieves an item from the local database that matches the description
    public Items GetItem(string pPlayerName, string pLocationID, string pItemName, float pPositionX)
    {
        string positionX = pPositionX.ToString();
        return _connection.Table<Items>().Where(x => x.PlayerName == pPlayerName && x.LocationID == pLocationID && x.Name == pItemName && x.PositionX == positionX).FirstOrDefault();
    }

    public List<Items> GetInventory(Player pPlayer)
    {
        return _connection.Table<Items>().Where(x => x.PlayerName == pPlayer.PlayerName && x.Picked == 1).ToList<Items>();
    }

    public Items GetItemToGive(string pPlayerName, string pItemName)
    {
        return _connection.Table<Items>().Where(x => x.PlayerName == pPlayerName && x.Name == pItemName && x.Picked == 1).FirstOrDefault();
    }

    public void DropItems()
    {
        _connection.DropTable<Items>();
        _connection.CreateTable<Items>();
    }

}
