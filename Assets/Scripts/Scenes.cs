using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ButtonScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
