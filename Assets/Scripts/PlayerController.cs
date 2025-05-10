using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Hurtbox hurtbox;
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private Rigidbody2D _rb;
    public Transform hand;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private float _horizontal;
    private BoxCollider2D _boxCollider;
    private Vector3 _originalScale;
    private const float CrouchCoefficient = 0.5f;
    private Vector2 _originalBoxSize;
    private bool _isCrouching;
    public Animator animator;
    public bool isBlocking;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _originalScale = transform.localScale;
        _originalBoxSize = _boxCollider.size;
    }

    private void FixedUpdate()
    {
        animator.SetBool(IsJumping, false);
        if(IsGrounded() && _horizontal != 0)
            animator.SetBool(IsWalking, true);
        else
            animator.SetBool(IsWalking, false);
        _rb.linearVelocity = new Vector2(_horizontal * moveSpeed, _rb.linearVelocity.y);
        hurtbox.currentStamina = Math.Min(hurtbox.maxStamina, hurtbox.currentStamina + 0.25f);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed || !IsGrounded() || _isCrouching) return;
        animator.SetBool(IsJumping, true);
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (animator.GetBool(IsJumping) || animator.GetBool(IsWalking)) //TODO: Прикрутить проверку на удар
            return;
        switch (context.phase)
        {
            case InputActionPhase.Started when IsGrounded() && !_isCrouching:
                isBlocking = true;
                // TODO: Прикрутить анимацию
                break;
            case InputActionPhase.Canceled when !isBlocking:
                return;
            case InputActionPhase.Canceled:
                isBlocking = false;
                //TODO: Прикрутить Анимацию
                break;
            case InputActionPhase.Disabled:
            case InputActionPhase.Waiting:
            case InputActionPhase.Performed:
                break;
            default:
                return;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started when IsGrounded():
                _isCrouching = true;
                transform.localScale = new Vector3(_originalScale.x, _originalScale.y * CrouchCoefficient, _originalScale.z);
                _boxCollider.size = new Vector2(_boxCollider.size.x, _boxCollider.size.y * CrouchCoefficient);
                transform.position = new Vector2(transform.position.x, transform.position.y - transform.localScale.y);
                break;
            case InputActionPhase.Canceled when !_isCrouching:
                return;
            case InputActionPhase.Canceled:
                _isCrouching= false;
                transform.position = new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2);
                transform.localScale = _originalScale;
                _boxCollider.size = _originalBoxSize;
                break;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
