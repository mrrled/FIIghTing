using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioSource audioClick;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        audioClick.Play();
    }
}
