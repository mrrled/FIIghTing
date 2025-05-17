using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioSource audioClick;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioClick.Play();
    }

    public void PlaySelectedSound()
    {
        audioSource.Play();
    }
}
