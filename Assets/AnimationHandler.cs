using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    #region Private & Const Variables
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Public Variables
    public bool isFlipOn;
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetAnimationType();
    }

    private void SetAnimationType(){
        Vector2 direction = rb.velocity.normalized;
        int runningType = 0;

        if (direction.y > 0.0f)
        {
            // Running Up
            runningType = 1;
        }
        else if (direction.y < 0.0f)
        {
            // Running Down
            runningType = 3;
        }
        else if (direction.x < 0.0f)
        {
            // Running Left
            runningType = 2;
        }
        else if (direction.x > 0.0f)
        {
            // Running Right
            runningType = 4;
        }

        // Start or stop running animation
        animator.SetInteger("RunningType", runningType);

        if (isFlipOn)
        {
            Flip(direction.x);
        }
    }

    private void Flip(float horizontalDirection)
    {
        bool flipSprite = spriteRenderer.flipX ? horizontalDirection < 0.0f : horizontalDirection > 0.0f;

        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    #endregion
}
