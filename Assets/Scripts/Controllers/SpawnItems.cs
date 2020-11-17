using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public static void LoadGameObjects()
    {
        //List<Player> cPlayerList = new List<Player>();
        //cPlayerList.Add(GameModel.cPlayer);
        //GameModel.ds.jsnGetPlayerLocationItems(cPlayerList);
        //System.Threading.Thread.Sleep(200);
        List<Items> locationItems = GameModel.ds.GetLocationItems(GameModel.cPlayer);
        foreach (Items item in locationItems)
        {
            float positionX = float.Parse(item.PositionX);
            float positionY = float.Parse(item.PositionY);
            Vector3 itemPosition = new Vector3(positionX, positionY, 0);
            Object newItem = Instantiate(Resources.Load("ItemPrefabs/" + item.Icon), itemPosition, Quaternion.identity);
            newItem.name = item.Name;
            Debug.Log("Loaded " + item.Name);
        }
    }
    public static void UnloadGameObjects()
    {
        List<Items> locationItems = GameModel.ds.GetLocationItems(GameModel.cPlayer);
        foreach (Items item in locationItems)
        {
            try
            {
                Destroy(GameObject.Find(item.Name));
                Debug.Log("Unloaded " + item.Name);
            }
            catch
            {
                Debug.Log("Already unloaded " + item.Name);
            }
            
        }
        GameModel.finished = true;
    }


}
