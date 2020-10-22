using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectGameModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            GameModel.Name = GameModel.ds.getGameExist().Name;
        }
        catch
        {
            GameModel.Name = "";
        }

        if (GameModel.Name == "")
        {
            GameModel.Name = "Vexed Text";
            GameModel.MakeGame();
        }
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            GameModel.storyHead = GameObject.Find("OutputField").GetComponent<Text>();
            GameModel.storyNarrative = GameObject.Find("StoryNarrative").GetComponent<Text>();
            GameModel.currentPlayer = GameObject.Find("Player");
            GameModel.loadLevel.StartLocation();
        }
        
    } 

}
