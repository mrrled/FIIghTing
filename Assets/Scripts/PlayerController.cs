using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform hand;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private float horizontal;
    private bool facingRight = true;
    private BoxCollider2D boxCollider;
    private Vector3 originalScale;
    private float crouchCoefficient = 0.5f;
    private Vector2 originalBoxSize;
    private bool isCrouching = false;
    public Animator animator;

    void Start()
    {
        // if (hand != null)
        //     facingRight = transform.position.x < hand.position.x;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalScale = transform.localScale;
        originalBoxSize = boxCollider.size;
    }
    
    private void FixedUpdate()
    {
        animator.SetBool("isJumping", false);
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        // if (facingRight && horizontal < 0 || !facingRight && horizontal > 0)
        //     Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isCrouching)
        {
            animator.SetBool("isJumping", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            isCrouching = true;
            transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchCoefficient, originalScale.z);
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y * crouchCoefficient);
            transform.position = new Vector2(transform.position.x, transform.position.y - transform.localScale.y);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if(!isCrouching)
                return;
            isCrouching= false;
            transform.position = new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2);
            transform.localScale = originalScale;
            boxCollider.size = originalBoxSize;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
