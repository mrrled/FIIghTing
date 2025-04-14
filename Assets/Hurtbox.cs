using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.transform.parent.name + " получил " + damage + " урона!");
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //exit or new round
        Debug.Log("Player died");
    }
}