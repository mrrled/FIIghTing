using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public Animator animator;
    public GameObject existingHitboxObject;
    public string animationTrigger = "Attack";
    public float attackDuration = 2f;

    void Start()
    {
        DeactivateHitbox();
    }

    public void Attacking(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            existingHitboxObject.transform.position = attackPoint.position;
            existingHitboxObject.transform.rotation = attackPoint.rotation;
            ActivateHitbox();
            animator.SetTrigger(animationTrigger);
            Invoke("DeactivateHitbox", attackDuration);
        }
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