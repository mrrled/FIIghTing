using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Hurtbox hurtbox;
    public bool isBlocking;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float repulsionForce = 50f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator animator;

    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsCrouching = Animator.StringToHash("isCrouching");
    private static readonly int IsWalkCrouching = Animator.StringToHash("isWalkCrouching");
    private static readonly int IsBlockingAnim = Animator.StringToHash("isBlocking");

    private Rigidbody2D _rb;
    private float _horizontal;
    private bool _isCrouching;
    private bool _isJumping;

    private bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    private bool IsMoving => !_rb.linearVelocity.Equals(Vector2.zero);

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Animate();
        //_rb.mass = IsMoving ? 1 : 1000;
        _isJumping = false;
        var velocityChange = _horizontal * moveSpeed - _rb.linearVelocity.x;
        _rb.AddForce(new Vector2(velocityChange * 5f, 0f), ForceMode2D.Force);
        if(!isBlocking)
            hurtbox.currentStamina = Math.Min(hurtbox.maxStamina, hurtbox.currentStamina + 0.25f);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed || !IsGrounded || _isCrouching) return;
        _isJumping = true;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started when IsGrounded:
                _isCrouching = true;
                break;
            case InputActionPhase.Canceled when !_isCrouching:
                return;
            case InputActionPhase.Canceled:
                _isCrouching = false;
                break;
        }
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (animator.GetBool(IsJumping) || animator.GetBool(IsWalking))
            return;
        switch (context.phase)
        {
            case InputActionPhase.Started when IsGrounded && !_isCrouching:
                isBlocking = true;
                break;
            case InputActionPhase.Canceled when !isBlocking:
                return;
            case InputActionPhase.Canceled:
                isBlocking = false;
                break;
            case InputActionPhase.Disabled:
            case InputActionPhase.Waiting:
            case InputActionPhase.Performed:
                break;
            default:
                return;
        }
    }

    public void Push(Vector2 pushFrom)
    {
        var pushDirection = ((Vector2)transform.position - pushFrom).normalized;
        //var tempo = _rb.mass;
        //_rb.mass = 1f;
        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(new Vector2(pushDirection.x * repulsionForce, 0) + Vector2.up * 10f, ForceMode2D.Impulse);
        //_rb.mass = tempo;
    }

    [Obsolete("Obsolete")]
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _horizontal = 0f;
            _rb.velocity = Vector2.zero; // Остановка персонажа при столкновении с другим персонажем
        }
    }

    private void Animate()
    {
        animator.SetBool(IsBlockingAnim, isBlocking);
        animator.SetBool(IsJumping, _isJumping);
        animator.SetBool(IsWalkCrouching, IsGrounded && _horizontal != 0 && _isCrouching);
        animator.SetBool(IsWalking, IsGrounded && _horizontal != 0 && !_isCrouching);
        animator.SetBool(IsCrouching, IsGrounded && _horizontal == 0 && _isCrouching);
    }
}