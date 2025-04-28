using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private Hurtbox _hurtboxPlayer1;
    private Hurtbox _hurtboxPlayer2;

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
            _hurtboxPlayer1.currentHealth = 100f;
        }
        else if (_hurtboxPlayer2.currentHealth <= 1e-10)
        {
            StartCoroutine(ChangeRound(0));
            _hurtboxPlayer2.currentHealth = 100f;
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ChangeRound(int winner)
    {
        // Здесь можно добавить анимацию или другие эффекты для смены раунда
        var roundNumber = PlayerPrefs.GetInt("roundNumber", 1);
        Debug.Log("Round " + roundNumber + " ended.");
        PlayerPrefs.SetInt("roundNumber", roundNumber + 1);
        Debug.Log("Starting Round " + (roundNumber + 1) + " ended.");
        PlayerPrefs.SetInt($"score{winner}", PlayerPrefs.GetInt($"score{winner}", 0) + 1);
        var (score0, score1) = (PlayerPrefs.GetInt("score0", 0), PlayerPrefs.GetInt("score1", 0));
        Debug.Log($"Score: {score0} - {score1}");
        yield return new WaitForSeconds(1f); // Задержка перед началом нового раунда
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
