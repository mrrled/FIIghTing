using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    //public PlayerHealth playerHealth;
    private float horizontal;
    private bool facingRight = true;
    private BoxCollider2D boxCollider;
    private Vector3 originalScale;
    private float crouchCoefficient = 0.5f;
    private Vector2 originalBoxSize;

    void Start()
    {
        var hand = GetComponentsInChildren<Transform>()
            .FirstOrDefault(x => Math.Abs(x.position.x - transform.position.x) > 1e-9);
        if (hand != null)
            facingRight = transform.position.x < hand.position.x;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalScale = transform.localScale;
        originalBoxSize = boxCollider.size;
        //playerHealth = GetComponent<PlayerHealth>();
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        if (facingRight && horizontal < 0 || !facingRight && horizontal > 0)
            Flip();
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
        if (context.performed && IsGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchCoefficient, originalScale.z);
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y * crouchCoefficient);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            transform.localScale = originalScale;
            boxCollider.size = originalBoxSize;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
