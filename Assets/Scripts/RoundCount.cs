using UnityEngine;
using UnityEngine.UI;

public class RoundCount : MonoBehaviour
{
    private Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Round: " + PlayerPrefs.GetInt("roundNumber", 1);
    }
}
