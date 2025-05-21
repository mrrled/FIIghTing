using UnityEngine;
using UnityEngine.UI;

public class RoundCount : MonoBehaviour
{
    private Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }
    
    void Update()
    {
        _text.text = "Round: " + PlayerPrefs.GetInt("roundNumber", 1);
    }
}
