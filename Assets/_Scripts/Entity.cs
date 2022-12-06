using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable
{
    [Header("Health Settings")]
    public int currentHealth;
    public int health;

    [Header("IA Settings")]
    [Range(.1f, 10)]
    public float radius;
    public LayerMask layerMask;
    public bool PlayerDetected;

    public bool isFacingRight;
    public float speed;
    public int meeleDamage;

    private void OnEnable()
    {
        currentHealth = health;
    }

    private void Update()
    {
        PerformDetection();
    }

    public virtual void TakeDamage(int Damage, Transform transform, float power)
    {
        currentHealth -= Damage;
        KnockBack(transform, power);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void KnockBack(Transform transform, float power)
    {
        GetComponent<Rigidbody2D>()
            .AddForce(
                (this.transform.position - transform.position).normalized * power,
                ForceMode2D.Impulse
            );
    }

    public virtual void Chase(GameObject player)
    {
        GetComponent<Rigidbody2D>().velocity =
            new Vector2(
                (player.transform.position.x - transform.position.x) * speed,
                player.transform.position.y - transform.position.y
            ).normalized * speed;

        if (player.transform.position.x < gameObject.transform.position.x && isFacingRight)
            Flip();
        if (player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
            Flip();
    }

    public virtual void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collider != null)
        {
            PlayerDetected = true;
            Chase(collider.gameObject);
        }
        else
        {
            PlayerDetected = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (PlayerDetected)
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public abstract void Die();
}
