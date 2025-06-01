using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    public Animator animator;
    
    private static readonly int StartAnimation = Animator.StringToHash("start");
    private static readonly int IsPressed = Animator.StringToHash("is_pressed");

    void Start()
    {
        var countRound = PlayerPrefs.GetInt("roundNumber");
        if (countRound == 1)
            animator.SetTrigger(StartAnimation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            animator.SetTrigger(IsPressed);
    }
}

