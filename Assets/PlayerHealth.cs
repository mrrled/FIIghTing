using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
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
