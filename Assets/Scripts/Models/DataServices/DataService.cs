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


    /* ====== */
	public void CreateDB(){
        // remove these once testing is sorted
       //_connection.DropTable<Location>(); 
       //_connection.DropTable<ToFrom>();
       //_connection.DropTable<Player>();
       //_connection.DropTable<Items>();

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
            EEntryY  = 00000000000000000000,
            Item1Name = "UUUUUUUUUUUUUUUUUUUU",
            Item1PositionX = 00000000000000000000,
            Item1PositionY = 00000000000000000000,
            Item2Name = "UUUUUUUUUUUUUUUUUUUU",
            Item2PositionX = 00000000000000000000,
            Item2PositionY = 00000000000000000000,
            Item3Name = "UUUUUUUUUUUUUUUUUUUU",
            Item3PositionX = 00000000000000000000,
            Item3PositionY = 00000000000000000000,
        }, jsnReceiverDel);
        

    }

    public void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
        // To do: parse and produce an appropriate response
    }

    public void jsnRegisterPlayer(List<Player> pReceivedList)
    {
        GameModel.jsDrop.Store<Player, JsnReceiver>(pReceivedList, jsnReceiverDel);
    }

    public void jsnGetPlayer(List<Player> pReceivedList)
    {
        GameModel.jsDrop.Select<Player, JsnReceiver>("PlayerName = '" + pReceivedList[0].PlayerName + "'", jsnPlayerListReceiverDel, jsnReceiverDel);
    }

    public void jsnPlayerListReceiverDel(List<Player> pReceivedList)
    {
        Debug.Log("Received items " + pReceivedList.Count);
        foreach (Player lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.PlayerName + "," + lcReceived.Password + "," + lcReceived.Score.ToString() + lcReceived.LocationId.ToString() + "}");
        
        }// for

        // To do: produce an appropriate response
    }


    public void jsnLocationListReceiverDel(List<Location> pReceivedList)
        {
            _connection.DeleteAll<Location>();
            _connection.InsertAll(pReceivedList);
        }

    // Locations and their relationships 
    public IEnumerable<Location> GetLocations()
    {
        return _connection.Table<Location>();
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

    //Items
    public Location AddItem(Location pLocation, string pItemName, float pPositionX, float pPositionY)
    {
        if (pLocation.Item1Name == null)
        {
            pLocation.Item1Name = pItemName;
            pLocation.Item1PositionX = pPositionX;
            pLocation.Item1PositionY = pPositionY;
        }
        else if (pLocation.Item2Name == null)
        {
            pLocation.Item2Name = pItemName;
            pLocation.Item2PositionX = pPositionX;
            pLocation.Item2PositionY = pPositionY;
        }
        else if (pLocation.Item3Name == null)
        {
            pLocation.Item3Name = pItemName;
            pLocation.Item3PositionX = pPositionX;
            pLocation.Item3PositionY = pPositionY;
        }
        _connection.Update(pLocation);
        return pLocation;
         
    }
    public Location RemoveItem(Location pLocation, string pItemName, float pItemPositionX)
    {
        if (pLocation.Item1Name == pItemName && pLocation.Item1PositionX == pItemPositionX)
        {
            pLocation.Item1Name = null;
            pLocation.Item1PositionX = 0;
            pLocation.Item1PositionY = 0;
        }
        else if (pLocation.Item2Name == pItemName && pLocation.Item2PositionX == pItemPositionX)
        {
            pLocation.Item2Name = null;
            pLocation.Item2PositionX = 0;
            pLocation.Item2PositionY = 0;
        }
        else if (pLocation.Item3Name == pItemName && pLocation.Item3PositionX == pItemPositionX)
        {
            pLocation.Item3Name = null;
            pLocation.Item3PositionX = 0;
            pLocation.Item3PositionY = 0;
        }
        _connection.Update(pLocation);
        return pLocation;
    }

    public void AddPlayerItem(Player pPlayer, Location pLocation, string pItemName)
    {
        Items newItem = new Items();
        newItem.PlayerName = pPlayer.PlayerName;
        newItem.LocationID = pLocation.Id;
        newItem.Name = pItemName;
        _connection.Insert(newItem);
    }

    }
