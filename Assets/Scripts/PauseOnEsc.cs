using UnityEngine;

public class PauseOnEsc : MonoBehaviour
{
    public GameObject panel;
    public bool isPaused;
    

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if(!isPaused)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    private void PauseGame(){
        panel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ContinueGame(){
        panel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
