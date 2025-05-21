using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private Text _text;
    public int player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
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
