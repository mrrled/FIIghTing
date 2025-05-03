using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("roundNumber", 1);
        PlayerPrefs.SetInt("score0", 0);
        PlayerPrefs.SetInt("score1", 0);
    }
}
