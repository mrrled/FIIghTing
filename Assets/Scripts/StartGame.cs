using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("roundNumber", 1);
        PlayerPrefs.SetInt("score0", 0);
        PlayerPrefs.SetInt("score1", 0);
    }
}
