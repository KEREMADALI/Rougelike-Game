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
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetAnimationType();
        position = transform.position;
    }

    private void SetAnimationType(){

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetFloat("x", direction.x > 0 ? 1 : -1);
            animator.SetFloat("y", 0);
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", direction.y > 0 ? 1 : -1);
        }
        else
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 0);
        }

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
