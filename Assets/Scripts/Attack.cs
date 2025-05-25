using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public Animator animator;
    public GameObject existingHitboxObject;
    public string animationTrigger = "Attack";
    public float attackDuration = 0.5f;
    private bool _isAttacking;

    void Start()
    {
        DeactivateHitbox();
    }

    public void Attacking(InputAction.CallbackContext context)
    {
        if (!context.performed || _isAttacking) return;
        existingHitboxObject.transform.position = attackPoint.position;
        existingHitboxObject.transform.rotation = attackPoint.rotation;
        ActivateHitbox();
        _isAttacking = true;
        animator.SetTrigger(animationTrigger);
        Invoke(nameof(DeactivateHitbox), attackDuration);
        _isAttacking = false;
    }

    void ActivateHitbox()
    {
        existingHitboxObject.SetActive(true);
    }

    void DeactivateHitbox()
    {
        existingHitboxObject.SetActive(false);
    }
}