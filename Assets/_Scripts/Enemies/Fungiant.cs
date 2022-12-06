using System.Collections;
using TopDown.Player;
using UnityEngine;

public class Fungiant : Entity
{
    Animator anim;
    private Rigidbody2D rb;
    private bool invencible = false;

    [SerializeField]
    private float invencibleTime = 0.5f;

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
        if (invencible)
        {
            return;
        }
        base.TakeDamage(Damage, transform, power);
        anim.SetTrigger("Hurt");
        StartCoroutine(Hit());
    }

    public override void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        collision.gameObject.GetComponent<Player>().TakeDamage(meeleDamage, transform, 0);
    }

    private IEnumerator Hit()
    {
        invencible = true;
        yield return new WaitForSeconds(invencibleTime);
        invencible = false;
    }
}
