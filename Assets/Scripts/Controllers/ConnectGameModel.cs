using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectGameModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameModel.Name != "Vexed Text")
        {

            GameModel.Name = "Vexed Text";
            GameModel.MakeGame();
            GameModel.storyHead = GameObject.Find("OutputField").GetComponent<Text>();
            GameModel.storyNarrative = GameObject.Find("StoryNarrative").GetComponent<Text>();
            GameModel.currentPlayer = GameObject.Find("Player");
        }
        GameModel.loadLevel.LoadLocation();
    } 

}
