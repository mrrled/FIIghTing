using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public string animationTrigger = "Attack";

    public void Attacking(InputAction.CallbackContext context)
    {
        animator.SetTrigger(animationTrigger);
    }
}