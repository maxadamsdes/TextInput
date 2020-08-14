using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectGameModel : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameModel.Name = "The Game Name";
        GameModel.MakeGame();
    }

}
