using System;
using UnityEngine;
using UnityEngine.UI;

public class Hurtbox : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.transform.parent.name + " получил " + damage + " урона!");
        currentHealth -= damage;
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