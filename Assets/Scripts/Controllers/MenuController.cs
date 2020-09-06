using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuButton;
    public InputField input;
    public Text textField;
    public GameObject playerObj;
    public GameObject audioOnIcon;
    public GameObject audioOffIcon;
    public GameObject joysticks;
    public string itemName;
    public string itemTag;
    private GameObject item;
    private bool isShowing = false;
    public Camera m_OrthographicCamera;
    float m_ViewPositionX, m_ViewPositionY;
    bool exit;
    static TouchScreenKeyboard touchScreenKeyboard;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        m_OrthographicCamera.enabled = true;
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = 5.0f;
        //touchScreenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
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
            //else
            //{
            //    // otherwise loads menu. To implement later
            //    loadMenu();
            //}
        }
    }

    public void openText()
    {
        if (isShowing != true)
        {
            //touchScreenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false);
            isShowing = true;
            menuButton.SetActive(false);
            joysticks.GetComponentInChildren<FloatingJoystick>().OnPointerUp2();
            joysticks.SetActive(false);
            menu.SetActive(true);
            input.ActivateInputField();
            playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            m_OrthographicCamera.orthographic = true;
            m_OrthographicCamera.orthographicSize = 4.0f;
            m_OrthographicCamera.transform.position += new Vector3(0, -5f, 0);
        }
    }

    public void exitText()
    {
        if (isShowing == true)
        {
            isShowing = false;
            textField.text = "";
            menu.SetActive(false);
            joysticks.SetActive(true);
            menuButton.SetActive(true);
            playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            playerObj.GetComponent<PlayerMovement>().enabled = true;
            playerObj.GetComponent<PlayerMovement>().locked = false;
            m_OrthographicCamera.orthographic = true;
            m_OrthographicCamera.orthographicSize = 5.0f;
            m_OrthographicCamera.transform.position += new Vector3(0, 5f, 0);
        }
        
    }

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
