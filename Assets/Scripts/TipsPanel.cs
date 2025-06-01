using UnityEngine;
using UnityEngine.EventSystems;

public class TipsPanel : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject buttonContinue;
    public GameObject buttonBack;
    
    public void Switch()
    {
        pausePanel.SetActive(false);
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttonBack);
    }

    public void Back()
    {
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttonContinue);
        gameObject.SetActive(false);
    }
}
