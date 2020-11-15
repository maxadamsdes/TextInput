using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public static void LoadGameObjects()
    {
        List<Items> locationItems = GameModel.ds.GetLocationItems(GameModel.cPlayer);
        foreach (Items item in locationItems)
        {
            Vector3 itemPosition = new Vector3(item.PositionX, item.PositionY, 0);
            Object newItem = Instantiate(Resources.Load("ItemPrefabs/" + item.Name), itemPosition, Quaternion.identity);
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
    }


}
