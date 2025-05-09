using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ButtonScene(int sceneId)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
