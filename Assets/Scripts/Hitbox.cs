using System;
using UnityEngine;

public enum HitboxType
{
    Hand,
    Leg
}

public class Hitbox : MonoBehaviour
{
    public HitboxType type;
    public GameObject owner;

    private float Damage
    {
        get
        {
            return type switch
            {
                HitboxType.Hand => 15f,
                HitboxType.Leg => 20f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    
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
        hurtbox?.TakeDamage(Damage, owner.transform.position);
    }
}