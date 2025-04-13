using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private BoxCollider2D _boxCollider;
    private float lastCrounch;
    
    private float horizontal;

    private Vector2 _maxHitboxHeight = new (1f, 1f);
    private Vector2 _crounchingHitboxHeight = new (1f, 0.5f);
    
    private bool _isCrouching = false;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        lastCrounch = Time.time;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        if (!Input.GetKeyDown(KeyCode.S))
        {
            var now = Time.time;
            if (!(now - lastCrounch >= 0.5)) return;
            //transform.position = new Vector3(0.8112521f, -2.82f, -0.0276221f);
            transform.localScale = new Vector3(1f, 2.325f, 1f);
            _isCrouching = false;
            return;
        }
        //if (!IsGrounded()) return;
        _isCrouching = true;
        lastCrounch = Time.time;
        if (_isCrouching)
        {
            transform.position -= new Vector3(0f, 0.5f, 0f);
            transform.localScale -= new Vector3(0f, 1f, 0f);
        }
        // else
        // {
        //     transform.position += new Vector3(0f, 0.5f, 0f);
        //     transform.localScale = new Vector3(1f, 2.325f, 1f);
        // }
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
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}