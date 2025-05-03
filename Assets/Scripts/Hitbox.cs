using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10;
    public GameObject owner; 

    void Start()
    {
        owner ??= transform.root.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Hurtbox")) return;
        if (collision.transform.root.gameObject == owner)
            return;
        var hurtbox = collision.GetComponent<Hurtbox>();
        hurtbox?.TakeDamage(damage);
    }
}