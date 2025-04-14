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

    void Start()
    {
        var hand = GetComponentsInChildren<Transform>()
            .FirstOrDefault(x => Math.Abs(x.position.x - transform.position.x) > 1e-9);
        if (hand != null)
            facingRight = transform.position.x < hand.position.x;
        rb = GetComponent<Rigidbody2D>();
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
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
