using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player1: MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    //public PlayerHealth playerHealth;
    private float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerHealth = GetComponent<PlayerHealth>();
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
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
