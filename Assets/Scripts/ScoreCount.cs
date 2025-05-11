using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private Text text;
    public int player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == 1)
            text.text = "score: " + PlayerPrefs.GetInt("score0", 0);
        if(player == 2)
            text.text = "score: " + PlayerPrefs.GetInt("score1", 0);
    }
}
