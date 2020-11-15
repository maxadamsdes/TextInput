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
            LocationId = 0,
            Password = "***************************"
        }, jsnReceiverDel);

        GameModel.jsDrop.Create<Location, JsnReceiver>(new Location
        {
            Id = 00000000000000000000,
            Name = "UUUUUUUUUUUUUUUUUUUU",
            Story = "UUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU",
            NEntryX = 00000000000000000000,
            SEntryX = 00000000000000000000,
            WEntryX = 00000000000000000000,
            EEntryX = 00000000000000000000,
            NEntryY = 00000000000000000000,
            SEntryY = 00000000000000000000,
            WEntryY = 00000000000000000000,
            EEntryY  = 00000000000000000000
        }, jsnReceiverDel);

        GameModel.jsDrop.Create<Items, JsnReceiver>(new Items
        {
            PlayerName = "UUUUUUUUUUUUUUUUUUUUUUUUU",
            Name = "UUUUUUUUUUUUUUUU",
            Icon = "UUUUUUUUUUUUUUUU",
            LocationID = 0,
            PositionX = 000000,
            PositionY = 000000,
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
            Debug.Log("Received {" + lcReceived.PlayerName + "," + lcReceived.LocationID.ToString() + "," + lcReceived.Name + "," + lcReceived.PositionX + "}");
        }
        _connection.DeleteAll<Items>();
        _connection.InsertAll(pReceivedList);
    }

    // Updates the servers database when an item is changed
    public void jsnUpdateItems(List<Items> pReceivedList)
    {
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
        return _connection.Table<Items>().ToList<Items>(); ;
    }

    public bool haveLocations()
    {
        return _connection.Table<Location>().Count() > 0;
    }

    public ToFrom GetToFrom(int pFromID, string pDirection)
    {
        return _connection.Table<ToFrom>().Where(tf => tf.FromID == pFromID
                                                      && tf.Direction == pDirection).FirstOrDefault();      
    }

    public Location GetLocation(int pLocationID)
    {
        return _connection.Table<Location>().Where(l => l.Id == pLocationID).FirstOrDefault();
    }

    public Location GetPlayerLocation (Player aPlayer)
    {
        return GetLocation(aPlayer.LocationId);
    }

    public void updatePlayerLocation(Player pPlayer)
    {
        _connection.InsertOrReplace(pPlayer);
        List<Player> PlayerList = new List<Player>();
        PlayerList.Add(GameModel.cPlayer);
        jsnStorePlayer(PlayerList);

    }

    public Location getGameExist()
    {
        return _connection.Table<Location>().Where(l => l.Id == 0).FirstOrDefault();
    }


    public Location storeNewLocation(string pName, string pStory, float pNEntryX, float pNEntryY, float pSEntryX, float pSEntryY, float pEEntryX, float pEEntryY, float pWEntryX, float pWEntryY)
    {
        Location newLocation = new Location
        {
            Name = pName,
            Story = pStory,
            NEntryX = pNEntryX,
            NEntryY = pNEntryY,
            SEntryX = pSEntryX,
            SEntryY = pSEntryY,
            WEntryX = pWEntryX,
            WEntryY = pWEntryY,
            EEntryX = pEEntryX,
            EEntryY = pEEntryY
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


    public void storeFromTo(int pFromID, int pToID, string pDirection)
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
    public Player storeNewPlayer(string pName, string pPassword, int pLocationId, int pScore)
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
        newItem.LocationID = pLocation.Id;
        newItem.Name = pItemName;
        _connection.Insert(newItem);
    }

    public void AddPlayerItems(Player pPlayer)
    {
        
        // Creates the first new Item
        Items item = new Items
        {
            PlayerName = pPlayer.PlayerName,
            LocationID = 0,
            Name = "Coin",
            Icon = "Coin",
            PositionX = -1f,
            PositionY = 0f,
            Picked = 0
        };

        // Where all other items are added by specifying their location, name, sprite and position
        item.addItem(pPlayer.PlayerName, 0, "Rune", "Rune", -2.456395f, -1.7982f, 0);
        item.addItem(pPlayer.PlayerName, 0, "Sword", "Sword", 0f, -10f, 0);
        item.addItem(pPlayer.PlayerName, 0, "Owlett_Monster", "Owlett_Monster", 9.8286f, 0.4f, 0);
        item.addItem(pPlayer.PlayerName, 1, "Chest", "Chest", 11.868f, 0.2910001f, 0);
        item.addItem(pPlayer.PlayerName, 1, "Coin", "Coin", 8.63f, -2.57f, 0);
        item.addItem(pPlayer.PlayerName, 2, "Chest", "Chest", 23.31f, -1.529251f, 0);
        item.addItem(pPlayer.PlayerName, 3, "Key", "Key", 0f, 1f, 0);
        item.addItem(pPlayer.PlayerName, 4, "Coin", "Coin", 0f, 1f, 0);
        item.addItem(pPlayer.PlayerName, 5, "Rune", "Rune", 0f, 1f, 0);
        item.addItem(pPlayer.PlayerName, 6, "Chest", "Chest", 0f, 1f, 0);
        item.addItem(pPlayer.PlayerName, 6, "Chest", "Chest", 0f, 2f, 0);
        item.addItem(pPlayer.PlayerName, 6, "Key", "Key", 0f, 3f, 0);
        // Creates a new list that new items can be added to
        List<Items> newItemList = new List<Items>();
        newItemList = GetAllLocationItems(pPlayer);
        jsnUpdateItems(newItemList);

    }

    // used for adding items to the database
    public Items storeNewPlayerItems(string pPlayerName, int pLocationID, string pItemName, string pIcon, float pPositionX, float pPositionY, int pPicked)
    {
        // Sets an item to match input
        Items newItem = new Items
        {
            PlayerName = pPlayerName,
            LocationID = pLocationID,
            Name = pItemName,
            Icon = pIcon,
            PositionX = pPositionX,
            PositionY = pPositionY,
            Picked = pPicked
        };
        // Stores new item into database
        _connection.Insert(newItem);
        // returns the item
        return newItem;
    }

    
    // Changes an items state to picked up
    public void Removeitem(string pPlayerName, int pLocationID, string pItemName, float pPositionX)
    {
        // creates a list that will be used for updating the server database later
        List<Items> itemList = new List<Items>();
        // Attempts to retrieve an item from the local DB using given attributes
        Items item = GetItem(pPlayerName, pLocationID, pItemName, pPositionX);
        // Changes the items state to be picked up
        item.Picked = 1;
        // Updates local db to match change
        _connection.InsertOrReplace(item);
        // Adds updated item to a list so that the jsnDrop is able to receive the data
        itemList.Add(item);
        jsnUpdateItems(itemList);
    }

    // Rertrieves an item from the local database that matches the description
    public Items GetItem(string pPlayerName, int pLocationID, string pItemName, float pPositionX)
    { 
        return _connection.Table<Items>().Where(x => x.PlayerName == pPlayerName && x.LocationID == pLocationID && x.Name == pItemName && x.PositionX == pPositionX).FirstOrDefault();
    }

}
