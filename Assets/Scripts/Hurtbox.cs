using System;
using UnityEngine;
using UnityEngine.UI;

public class Hurtbox : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.transform.parent.name + " получил " + damage + " урона!");
        currentHealth = Math.Max(0f, currentHealth - damage);
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}