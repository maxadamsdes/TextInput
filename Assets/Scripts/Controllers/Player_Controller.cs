using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    private SpriteRenderer spriteRenderer;
    public float playerJumpForce = 500f;
    private float playerHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }

    private void FixedUpdate()
    {


        //Obsolete, makes the player move around the screen!
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movement * Time.deltaTime * moveSpeed;

        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            myRigidBody.velocity = new Vector2(2, 0);
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            myRigidBody.velocity = new Vector2(-2, 0);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            playerHurtTime = Time.time;
            myAnim.SetBool("playerHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * playerJumpForce);
            myCollider.enabled = false;
            deathSfx.Play();

            float currentBestScore = PlayerPrefs.GetFloat("BestScore", 0);
            float currentScore = Time.time - startTime;

            if (currentScore > currentBestScore)
            {
                PlayerPrefs.SetFloat("BestScore", currentScore);
            }
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }
    }
}
