using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private Vector3 startingPosition;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    //Sets the camera and sprite so that they can be changed
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Transform spritePosition = GetComponent<SpriteRenderer>().transform;
        spritePosition.position = new Vector3(0 + startingPosition.x, 0 + startingPosition.y, 0 + startingPosition.z);
    }

    //Moves the background based on the players direction and the multipliers added to each object
    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;
    }
}
