﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PasswordRegController : MonoBehaviour
{
    public GameObject RegPanel;
    public GameObject PasspromptPanel;
    public InputField Playername;
    public InputField Password;

    private void HidePanels()
    {
        RegPanel.SetActive(false);
        PasspromptPanel.SetActive(false);
    }
    private void ShowRegPanel()
    {
        RegPanel.SetActive(true);
        PasspromptPanel.SetActive(false);
    }

    private void ShowPasspromptPanel()
    {
        RegPanel.SetActive(false);
        PasspromptPanel.SetActive(true);
    }

    public void TryAgain()
    {
        HidePanels();
    }
    public void CheckPassword()
    {
        HidePanels();
        switch (GameModel.CheckPassword(Playername.text, Password.text))
        {
            case GameModel.PasswdMode.OK:
                HidePanels();
                Player pPlayer = GameModel.ds.getPlayer(Playername.text);
                List<Player> pPlayerList = new List<Player>();
                pPlayerList.Add(pPlayer);
                GameModel.ds.jsnGetPlayerLocationItems(pPlayerList);
                SceneManager.LoadScene("Game");
                break;
            case GameModel.PasswdMode.NeedName:
                ShowRegPanel();
                break;
            case GameModel.PasswdMode.NeedPassword:
                ShowPasspromptPanel();
                break;

        }

    }

    public void RegisterPlayer()
    {
        GameModel.RegisterPlayer(Playername.text, Password.text);
        HidePanels();
        SceneManager.LoadScene("Game");
    }
    // Start is called before the first frame update
    void Start()
    {
        RegPanel.SetActive(false);
        PasspromptPanel.SetActive(false);
        GameModel.ds.jsnGetAllPlayers();

        //------------------------------------------------Only used for testing!!!!!!!!!!!!!!!!!------------------------------------------------
        //
        //GameModel.CheckPassword("max", "adams"); 
        //HidePanels();
        //SceneManager.LoadScene("Game");
        //
    }
}
