using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadGameObjects();   
    }
    public static void LoadGameObjects()
    {
        foreach (KeyValuePair<string, Vector3> entry in GameModel.currentLocale.Items)
        {
            Object newObject = Instantiate(Resources.Load("ItemPrefabs/" + entry.Key), entry.Value, Quaternion.identity);
            newObject.name = entry.Key;
        }
    }

    public static void DestroyGameObjects()
    {
        foreach (KeyValuePair<string, Vector3> entry in GameModel.currentLocale.Items)
        {
            GameObject.Find(entry.Key).SetActive(false);
        }
    }

}
