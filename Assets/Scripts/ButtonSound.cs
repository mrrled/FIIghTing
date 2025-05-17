using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
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
        audioSource.PlayOneShot(audioSource.clip);
        _animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger("Normal");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioClick.Play();
    }
}
