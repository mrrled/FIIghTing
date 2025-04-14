using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
<<<<<<< Updated upstream
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    
=======
    public float maxHealth = 100;
    public float currentHealth;
>>>>>>> Stashed changes
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
<<<<<<< Updated upstream
    public void TakeDamage(float damage)
=======
    public void TakeDamage(int damage)
>>>>>>> Stashed changes
    {
        //animation take damage
        currentHealth = Math.Max(0f, currentHealth - damage);
        healthBar.fillAmount -= damage / maxHealth;
        if (currentHealth <= 1e-10)
            Die();
    }
    void Die()
    {
        //exit or new round
        Debug.Log("Player died");
    }
}
