using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Scenes
//Main 0  About 1  DevScene 2  Win 3  Lose 4  Options 5


// =================NOTE=====================
// The ground checker only looks at objects tagged
// with the "Ground" layer (to keep it from checking 
// itself). Any terrain you want the player to walk
// on should have the "Ground" layer on them.

public class PlayerMovement : MonoBehaviour
{
    //..............................................Variables
    public CharacterController controller;

    //..............................................Movement 
    public float moveSpeed = 10f;

    public float gravity = -9.81f;
    Vector3 fallVelocity;

    public Transform groundCheck;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float jumpStrength = 3f;

    public AudioSource JumpSound;

    void Update()
    {
        //..............................................Ground check/Fall velocity reset
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        //resets fall velocity to a small number if close
        if(isGrounded && fallVelocity.y < 0)
        {
            fallVelocity.y = -2f;
        }
        //..............................................Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //..............................................Move player
        //creates a location to move to relative to 
        //direction player is facing
        Vector3 newLocation = transform.right * x + transform.forward * z;

        controller.Move(newLocation * moveSpeed * Time.deltaTime);

        //..............................................Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            JumpSound.Play();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //..............................................Player fall velocity
        //Note: multiplied by Time.deltaTime twice 
        //because that's just how physics works man
        fallVelocity.y = fallVelocity.y + gravity * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        fallVelocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
    }
    

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bad Ground"))
        {
            SceneManager.LoadScene(6);
        }
    }
}
