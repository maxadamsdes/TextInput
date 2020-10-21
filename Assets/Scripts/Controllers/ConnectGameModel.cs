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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            if (GameModel.Name != "Vexed Text")
            {
                GameModel.Name = "Vexed Text";
                GameModel.MakeGame();
                GameModel.storyHead = GameObject.Find("OutputField").GetComponent<Text>();
                GameModel.storyNarrative = GameObject.Find("StoryNarrative").GetComponent<Text>();
                GameModel.currentPlayer = GameObject.Find("Player");
                GameModel.ds.getPlayer(GameModel.cPlayer.PlayerName);
            }
            GameModel.loadLevel.StartLocation();
        }
        
    } 

}
