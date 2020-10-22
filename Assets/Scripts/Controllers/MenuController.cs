using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public GameObject audioOnIcon;
    public GameObject audioOffIcon;
    public string itemName;
    public string itemTag;
    private bool isShowing = false;
    float m_ViewPositionX, m_ViewPositionY;
    bool exit;

    void Start()
    {
        GameModel.m_OrthographicCamera.enabled = true;
        GameModel.m_OrthographicCamera.orthographic = true;
        GameModel.m_OrthographicCamera.orthographicSize = 5.0f;
        isShowing = false;
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.Escape)))// || (touchScreenKeyboard.status == TouchScreenKeyboard.Status.LostFocus))
        {
            if(isShowing == true)
            {
                // closes text inputs if open
                exitText();
            }
        }
    }
    //Opens "text input" panel and disables movement
    public void openText()
    {
        if (isShowing != true)
        {
            //touchScreenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false);
            isShowing = true;
            GameModel.menuButton.SetActive(false);
            GameModel.joysticks.GetComponentInChildren<FloatingJoystick>().OnPointerUp2();
            GameModel.joysticks.SetActive(false);
            GameModel.input.SetActive(true);
            GameModel.textInput.ActivateInputField();
            GameModel.currentPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            GameModel.m_OrthographicCamera.orthographic = true;
            GameModel.m_OrthographicCamera.orthographicSize = 4.0f;
            GameModel.m_OrthographicCamera.transform.position += new Vector3(0, -5f, 0);
        }
    }

    //Closes the "text input" panel and reenables movement
    public void exitText()
    {
        isShowing = false;
        GameModel.textInput.text = "";
        GameModel.input.SetActive(false);
        GameModel.joysticks.SetActive(true);
        GameModel.menuButton.SetActive(true);
        GameModel.currentPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameModel.currentPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GameModel.currentPlayer.GetComponent<PlayerMovement>().enabled = true;
        GameModel.currentPlayer.GetComponent<PlayerMovement>().locked = false;
        GameModel.m_OrthographicCamera.orthographic = true;
        GameModel.m_OrthographicCamera.orthographicSize = 5.0f;
        GameModel.m_OrthographicCamera.transform.position += new Vector3(0, 5f, 0);
        
    }

    // toggles the sound state to muted or on
    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
        SetSoundState();
    }

    // changes the state of sound to mute or unmute
    private void SetSoundState()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            audioOnIcon.SetActive(true);
            audioOffIcon.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            audioOnIcon.SetActive(false);
            audioOffIcon.SetActive(true);
        }

    }

}
