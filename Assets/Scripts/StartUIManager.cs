using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        var countRound = PlayerPrefs.GetInt("roundNumber");
        if (countRound == 1)
            animator.SetTrigger("start");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            animator.SetTrigger("is_pressed");
    }
}

