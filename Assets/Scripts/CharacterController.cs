using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private const float p_Speed = 5.0f;
    private int runningType = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Weapon weapon;


    private void Awake() {
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        weapon = this.GetComponentInChildren<Weapon>();
    }

    private void FixedUpdate()
    {
        Vector2 inputMovementVector =  GetMovementInput();
        Vector3 inputShootingVector = GetShootingInput();

        Flip(inputMovementVector.x);
        Move(inputMovementVector);

        if(!inputShootingVector.Equals(Vector3.zero))
            Shoot(inputShootingVector);
    }
    // Updates shootInput if inputVector not zero
    private void Shoot(Vector3 inputShootingVector)
    {
        weapon.Shoot(inputShootingVector);
    }

    // Listens Arrow keys and returns a Vector3
    private Vector3 GetShootingInput()
    {
        Vector3 retVal = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            retVal = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            retVal = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            retVal = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            retVal = Vector3.left;
        }

        return retVal;
    }

    // Reads keyboard commands
    private Vector2 GetMovementInput() {
        int x = 0;
        int y = 0;

        if (Input.GetKey(KeyCode.W))
        {
            y++;
        }

        if (Input.GetKey(KeyCode.S))
        {
            y--;
        }

        if (Input.GetKey(KeyCode.D))
        {
            x++;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x--;
        }

        Vector2 retVal = new Vector2(x,y);

        return retVal;
    }

    // Flips the character if necessary
    private void Flip(float horizontalDirection)
    {
        bool flipSprite = spriteRenderer.flipX ? horizontalDirection < 0.0f : horizontalDirection > 0.0f;

        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // Controls character movement and animation
    private void Move(Vector2 inputVector) {

        if (inputVector.y > 0.0f)
        {
            // Running Up
            runningType = 1;
        }
        else if (inputVector.y < 0.0f)
        {
            // Running Down
            runningType = 3;
        }
        else if (inputVector.x != 0.0f)
        {
            // Running SideWays
            runningType = 2;
        }
        else {
            // Idle
            runningType = 0;
        }

        // Start or stop running animation
        animator.SetInteger("RunningType", runningType);

        transform.Translate(inputVector * p_Speed * Time.fixedDeltaTime);
    }

}
