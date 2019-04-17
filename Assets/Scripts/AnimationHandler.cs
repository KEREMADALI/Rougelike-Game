using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    #region Private & Const Variables
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 position;
    #endregion

    #region Public Variables
    public bool isFlipOn;
    public Vector2 direction;
    public int runningType;
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        position = transform.position;
    }

    private void Update()
    {
        SetAnimationType();
        position = transform.position;
    }

    private void SetAnimationType(){
        direction = (Vector2) transform.position - position;
        runningType = 0;

        if (direction.y > 0.05f)
        {
            // Running Up
            runningType = 1;
        }
        else if (direction.y < -0.05f)
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
