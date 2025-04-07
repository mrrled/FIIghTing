using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float damage = 20f;

    public float attackRange = 0.5f;

    public LayerMask playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Attack(InputAction.CallbackContext context)
    {
        //animation attack
        if (context.performed)
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            if (hitPlayer is null) return;
            hitPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("We hit player" + hitPlayer.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
