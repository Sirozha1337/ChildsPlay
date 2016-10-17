﻿using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerPhysicsScript : MonoBehaviour
{
    /// <summary>
    /// Moving Speed of the character
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    // Movement Vector
    private Vector2 movement;

    // Store RigidBody component
    private Rigidbody2D rigidbodyComponent;

    // Store SpriteRenderer component
    private SpriteRenderer spriterendererComponent;

    // Bool variable to check if the object is standing on the ground and can jump
    [HideInInspector]
    public bool canJump = true;

    // Variables for unlocking abilities
    public bool unlockJump;
    public bool unlockLeft;
    public bool unlockKick;

    void OnTriggerEnter2D(Collider2D other)
    {
        /*foreach (ContactPoint2D contact in other.contacts)
        {
            if (contact.point.y < transform.position.y){
                canJump = true;
            }
        }*/
		canJump = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canJump = false;
    }

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        spriterendererComponent = GameObject.Find("body").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        
        if(!unlockLeft && inputX < 0)
        {
            inputX *= -1;
        }

        // Set the X component for the movement vector
        movement = new Vector2(
          speed.x * inputX,
          rigidbodyComponent.velocity.y);

        // Check where our hero is facing
        if(inputX > 0)
        {
            spriterendererComponent.flipX = false;
        }

        if (inputX < 0)
        {
            spriterendererComponent.flipX = true;
        }


        // Set the y component of the movement vector
        if (unlockJump && Input.GetButton("Jump") && canJump)
		{
			rigidbodyComponent.AddForce(Vector2.up * speed.y);
        }
    }

    void FixedUpdate()
    {
        // Move the game object
        rigidbodyComponent.AddForce(movement * speed.x);
    }
}