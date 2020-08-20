using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    public GameObject playerObj;
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        playerObj = GameObject.Find("Player");
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
        playerObj.GetComponent<PlayerMovement>().joystickDown = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
        playerObj.GetComponent<PlayerMovement>().joystickDown = false;
    }
    public override void OnPointerUp2()
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp2();
        playerObj.GetComponent<PlayerMovement>().joystickDown = false;
    }
}