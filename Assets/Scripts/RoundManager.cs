using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    private Hurtbox _hurtboxPlayer1;
    private Hurtbox _hurtboxPlayer2;

    void Start()
    {
        var player1 = GameObject.FindWithTag("Hurtbox_Player1");
        var player2 = GameObject.FindWithTag("Hurtbox_Player2");
        _hurtboxPlayer1 = player1.GetComponent<Hurtbox>();
        _hurtboxPlayer2 = player2.GetComponent<Hurtbox>();
    }
    
    void Update()
    {
        if (_hurtboxPlayer1.currentHealth <= 1e-10)
        {
            StartCoroutine(ChangeRound(1));
            _hurtboxPlayer1.currentHealth = 1f;//TODO: Убрать этот костыль
        }
        else if (_hurtboxPlayer2.currentHealth <= 1e-10)
        {
            StartCoroutine(ChangeRound(0));
            _hurtboxPlayer2.currentHealth = 1f;//TODO: убрать этот костыль
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private static IEnumerator ChangeRound(int winner)
    {
        var roundNumber = PlayerPrefs.GetInt("roundNumber", 1);
        PlayerPrefs.SetInt("roundNumber", roundNumber + 1);
        PlayerPrefs.SetInt($"score{winner}", PlayerPrefs.GetInt($"score{winner}", 0) + 1);
        
        var (score0, score1) = (PlayerPrefs.GetInt("score0", 0), PlayerPrefs.GetInt("score1", 0));
        ChangeRoundLog(roundNumber, score0, score1);
        
        yield return new WaitForSeconds(1f); // Задержка перед началом нового раунда
        LoadNextScene(score0, score1);
    }

    private static void ChangeRoundLog(int roundNumber, int score0, int score1)
    {
        Debug.Log("Round " + roundNumber + " ended.");
        Debug.Log("Starting Round " + (roundNumber + 1) + " ended.");
        Debug.Log($"Score: {score0} - {score1}");
    }

    private static void LoadNextScene(int score0, int score1)
    {
        SceneManager.LoadScene((score0 >= 2 || score1 >= 2)
            ? 4 // TODO: Придумать как избавиться от хардкода
            : SceneManager.GetActiveScene().buildIndex);
    }
}
