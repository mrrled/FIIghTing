using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Hurtbox hurtbox;
    public bool isBlocking;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator animator;
    
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsCrouching = Animator.StringToHash("isCrouching");
    private static readonly int IsWalkCrouching = Animator.StringToHash("isWalkCrouching");
    private Rigidbody2D _rb;
    public Transform hand;
    private float _horizontal;
    private BoxCollider2D _boxCollider;
    private Vector3 _originalScale;
    private const float CrouchCoefficient = 0.5f;
    private Vector2 _originalBoxSize;
    private bool _isCrouching;
    private bool _isJumping;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _originalScale = transform.localScale;
        _originalBoxSize = _boxCollider.size;
    }

    private void FixedUpdate()
    {
        Animate();
        _isJumping = false;
        _rb.linearVelocity = new Vector2(_horizontal * moveSpeed, _rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed || !IsGrounded() || _isCrouching) return;
        _isJumping = true;
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
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
                _isCrouching = false;
                break;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }

    private void Animate()
    {
        animator.SetBool(IsJumping, _isJumping);
        animator.SetBool(IsWalkCrouching, IsGrounded() && _horizontal != 0 && _isCrouching);
        animator.SetBool(IsWalking, IsGrounded() && _horizontal != 0 && !_isCrouching);
        animator.SetBool(IsCrouching, IsGrounded() && _horizontal == 0 && _isCrouching);
    }
}
