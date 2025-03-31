using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool isGrounded; // Проверка, находится ли объект на земле 
    public Transform groundCheck; // Объект для проверки земли (например, пустой GameObject)
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround; // Слой, представляющий землю
    public string HorizontalAxisName = "Horizontal";
    public string JumpName = "Jump";

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Проверяем, находится ли объект на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Прыжок
        if (isGrounded && Input.GetButtonDown(JumpName)) // GetButtonDown позволяет настраивать кнопку в Input Manager
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(HorizontalAxisName);

        // Создаем вектор скорости
        Vector2 velocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Устанавливаем скорость Rigidbody2D
        rb.linearVelocity = velocity;
    }
}