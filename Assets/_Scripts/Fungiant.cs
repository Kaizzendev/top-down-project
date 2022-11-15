using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungiant : Entity
{

    Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        anim.SetFloat("speed", rb.velocity.magnitude);
    }

    public override void TakeDamage(int Damage, Transform transform, float power)
    {
        base.TakeDamage(Damage,transform,power);
        anim.SetTrigger("Hurt");
    }

    public override void KnockBack(Transform transform, float power)
    {
        base.KnockBack(transform, power);
    }


    public override void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }


    public override void Chase(GameObject player)
    {
        base.Chase(player);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        collision.gameObject.GetComponent<Player>().TakeDamage(meeleDamage, transform, 0);

    }
}
