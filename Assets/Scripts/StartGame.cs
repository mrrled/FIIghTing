using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        PlayerPrefs.SetInt("roundNumber", 1);
        PlayerPrefs.SetInt("score0", 0);
        PlayerPrefs.SetInt("score1", 0);
    }
}
