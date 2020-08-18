﻿using System.Collections;
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
    public string itemName;
    public string itemTag;
    private GameObject item;
    private bool isShowing = false;
    public Camera m_OrthographicCamera;
    float m_ViewPositionX, m_ViewPositionY;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        m_OrthographicCamera.enabled = true;
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = 5.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isShowing == true)
            {
                // closes text inputs if open
                exitText();
            }
            else
            {
                // otherwise loads menu
                loadMenu();
            }
            
        }
    }

    public void openText()
    {
        if (isShowing != true)
        {
            isShowing = true;
            menuButton.SetActive(false);
            menu.SetActive(true);
            input.ActivateInputField();
            playerObj.GetComponent<PlayerMovement>().enabled = false;
            playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            m_OrthographicCamera.orthographic = true;
            m_OrthographicCamera.orthographicSize = 4.0f;
        }
    }

    public void exitText()
    {
        isShowing = false;
        textField.text = "";
        menu.SetActive(false);
        menuButton.SetActive(true);
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        playerObj.GetComponent<PlayerMovement>().enabled = true;
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = 5.0f;
    }


    void loadMenu()
    {
        // menu screen for later
    }

    void OnTriggerEnter2D(Collider2D encounter)
    {
        if (encounter.gameObject.tag == "Encounter")
        {

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
