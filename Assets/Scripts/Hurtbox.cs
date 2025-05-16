#define __DEBUG__

using System;
using UnityEngine;
using UnityEngine.UI;

public class Hurtbox : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public Image staminaBar; //TODO: Прикрутить картинку Стамина Бара
    public float maxStamina = 100f;
    public float currentStamina;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void TakeDamage(float damage, Vector2 pushFrom)
    {
        var playerController = gameObject.transform.parent.GetComponent<PlayerController>();
        playerController.Push(pushFrom);
        if (playerController.isBlocking)
            damage = Math.Max(0f, damage - BlockDamage(damage));
        if (damage == 0) return;
        Debug.Log(gameObject.transform.parent.name + " получил " + damage + " урона!");
        currentHealth = Math.Max(0f, currentHealth - damage);
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private float BlockDamage(float damage)
    {
        Debug.Log(gameObject.transform.parent.name + " заблокировал " + damage + " урона!");
        var blockedDamage = Math.Min(damage, currentStamina / 2);
        currentStamina = Math.Max(0f, currentStamina - 2 * damage);
        //TODO: Убрать этот иф когда будет картинка стамина-бара
        #if __DEBUG__
        #else
        staminaBar.fillAmount = currentStamina / maxStamina;
        #endif
        return blockedDamage;
    }
}