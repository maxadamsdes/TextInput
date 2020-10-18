using System.Collections;
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
                SceneManager.LoadScene("TutMS2");
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
        SceneManager.LoadScene("TutMS2");
    }
    // Start is called before the first frame update
    void Start()
    {
        RegPanel.SetActive(false);
        PasspromptPanel.SetActive(false);
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
