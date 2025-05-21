using System;
using UnityEngine;
using UnityEngine.UI;

public class Hurtbox : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public Image staminaBar;
    public float maxStamina = 100f;
    public float currentStamina;
    public AudioSource attackSound;
    public Animator animator;
    public string receiveDamageTrigger = "hit";

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    private void FixedUpdate() {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void TakeDamage(float damage, Vector2 pushFrom)
    {
        var playerController = gameObject.transform.parent.GetComponent<PlayerController>();
        
        if (playerController.isBlocking)
            damage = Math.Max(0f, damage - BlockDamage(damage));
        
        if (damage == 0) return;
        
        attackSound.Play();
        playerController.Push(pushFrom);
       
        Debug.Log(gameObject.transform.parent.name + " получил " + damage + " урона!");
        
        currentHealth = Math.Max(0f, currentHealth - damage);
        healthBar.fillAmount = currentHealth / maxHealth;
        animator.SetTrigger(receiveDamageTrigger);
    }

    private float BlockDamage(float damage)
    {
        Debug.Log(gameObject.transform.parent.name + " заблокировал " + damage + " урона!");
        var blockedDamage = Math.Min(damage, currentStamina / 2);
        currentStamina = Math.Max(0f, currentStamina - 2 * damage);
        return blockedDamage;
    }
}