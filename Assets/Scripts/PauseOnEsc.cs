using UnityEngine;

public class PauseOnEsc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panel;
    public bool IsPaused;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!IsPaused)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    public void PauseGame(){
        panel.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ContinueGame(){
        panel.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }
}
