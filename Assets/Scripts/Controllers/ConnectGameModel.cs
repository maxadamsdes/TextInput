using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectGameModel : MonoBehaviour
{
    public InputField inputField;
    public GameObject input;
    public GameObject joysticks;
    public GameObject menuButton;
    public Camera m_OrthographicCamera;

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
            GameModel.m_OrthographicCamera = m_OrthographicCamera;
            GameModel.joysticks = joysticks;
            GameModel.menuButton = menuButton;
            GameModel.input = input;
            GameModel.textInput = inputField;
            GameModel.storyHead = GameObject.Find("OutputField").GetComponent<Text>();
            GameModel.storyNarrative = GameObject.Find("StoryNarrative").GetComponent<Text>();
            GameModel.currentPlayer = GameObject.Find("Player");
            GameModel.loadLevel.StartLocation();
        }
        
    } 

}
