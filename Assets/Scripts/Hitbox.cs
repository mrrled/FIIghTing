using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage = 10;
    public GameObject owner;

    void Start()
    {
        owner ??= transform.root.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Hurtbox") ||
            collision.gameObject.transform.parent.gameObject == owner)
            return;
        var hurtbox = collision.GetComponent<Hurtbox>();
        hurtbox?.TakeDamage(damage, owner.transform.position);
    }
}