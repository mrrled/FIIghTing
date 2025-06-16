using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject settingsButton;
    public GameObject startButton;
    public GameObject panel;
    
    public void Open()
    {
        if (panel != null)
            panel.SetActive(false);
        settingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsButton);
    }

    public void Close()
    {
        settingsPanel.SetActive(false);
        if (panel != null)
            panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startButton);
    }
}
