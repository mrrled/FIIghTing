using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private Hurtbox _hurtboxPlayer1;
    private Hurtbox _hurtboxPlayer2;

    private int _currentRound = 1;

    public int[] score = {0, 0};

    void Start()
    {
        _hurtboxPlayer1 = player1.GetComponent<Hurtbox>();
        _hurtboxPlayer2 = player2.GetComponent<Hurtbox>();
    }

    void Update()
    {
        if (_hurtboxPlayer1.currentHealth <= 1e-10)
        {
            StartCoroutine(ChangeRound(1));
        }
        else if (_hurtboxPlayer2.currentHealth <= 1e-10)
        {
            StartCoroutine(ChangeRound(0));
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ChangeRound(int winner)
    {
        // Здесь можно добавить анимацию или другие эффекты для смены раунда
        Debug.Log("Round " + _currentRound + " ended.");

        // Восстановление здоровья игроков
        _hurtboxPlayer1.currentHealth = _hurtboxPlayer1.maxHealth;
        _hurtboxPlayer2.currentHealth = _hurtboxPlayer2.maxHealth;
        _hurtboxPlayer1.healthBar.fillAmount = 1;
        _hurtboxPlayer2.healthBar.fillAmount = 1;
        _currentRound++;
        score[winner]++;
        Debug.Log("Starting Round " + _currentRound);
        Debug.Log($"Score: {score[0]} - {score[1]}");
        yield return new WaitForSeconds(5f); // Задержка перед началом нового раунда
    }
}
