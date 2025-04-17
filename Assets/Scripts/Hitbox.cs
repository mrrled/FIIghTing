using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10;
    public GameObject owner; 

    void Start()
    {
        if (owner == null)
        {
            owner = transform.root.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hurtbox"))
        {
            if (collision.transform.root.gameObject == owner) return;

            Hurtbox hurtbox = collision.GetComponent<Hurtbox>();
            if (hurtbox != null)
            {
                hurtbox.TakeDamage(damage); 
            }
        }
    }
}