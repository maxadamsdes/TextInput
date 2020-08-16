
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	Animator animator;
	public CharacterController2D controller;
	public float runSpeed = 40f; //
	public float horizontalMove = 0f;
	public bool lockMove = false;
	bool jump = false;
	bool crouch = false;
	public float playerPositionX;
	public float playerPositionY;
	private GameObject playerObj = null; //
	//protected Joystick joystick;
	//protected Joy joybutton;

	void Start()
    {
		animator = GetComponent<Animator>();
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}
    }

	// Update is called once per frame
	void Update()
	{
		//var rigidbody2D = GetComponent<Rigidbody2D>();
		//rigidbody2D.velocity = new Vector3(joystick.Horizontal * 30f,
		//									rigidbody2D.velocity.y,
		//									joystick.Vertical * 30f);
		

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool fire = Input.GetButtonDown("Fire1");
		if (h != 0)
        {
			animator.SetBool("horizontalMovement", true);
		}
        else
        {
			animator.SetBool("horizontalMovement", false);
		}
		if (v > 0.2)
		{
			animator.SetBool("verticalMovement", true);
		}
		else
		{
			animator.SetBool("verticalMovement", false);
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		playerPositionX = playerObj.transform.position.x;
		playerPositionY = playerObj.transform.position.y;
	
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		//if (Input.GetButtonDown("Cancel"))
  //      {
		//	SceneManager.LoadScene("Story");
  //      }
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}