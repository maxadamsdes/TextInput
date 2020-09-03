using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectGameModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameModel.Name != "Vexed Text")
        {
            GameModel.Name = "Vexed Text";
            GameModel.MakeGame();
            GameModel.itemList = Resources.LoadAll<GameObject>("file:../Assets/Prefabs/Items");
        }
    } 

}
