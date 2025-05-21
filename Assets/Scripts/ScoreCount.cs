using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private Text _text;
    public int player;
    void Start()
    {
        _text = GetComponent<Text>();
    }
    
    void Update()
    {
        _text.text = player switch
        {
            1 => "score: " + PlayerPrefs.GetInt("score0", 0),
            2 => "score: " + PlayerPrefs.GetInt("score1", 0),
            _ => _text.text
        };
    }
}
