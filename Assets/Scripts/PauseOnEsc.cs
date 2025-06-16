using UnityEngine;
using UnityEngine.EventSystems;

public class PauseOnEsc : MonoBehaviour
{
    public GameObject panel;
    public GameObject tipsPanel;
    public GameObject continueButton;
    public GameObject settingsPanel;
    public bool isPaused;
    

    void Update()
    {
        if (!Input.GetButtonDown("Cancel")) return;
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
        EventSystem.current.SetSelectedGameObject(continueButton);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ContinueGame(){
        panel.SetActive(false);
        tipsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
