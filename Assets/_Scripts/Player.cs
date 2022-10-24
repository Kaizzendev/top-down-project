using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;

public class Player : Entity
{
    // Movement
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private bool facingRight = true;
    private Vector2 moveDirection;

    // Animation
    private Animator animator;

    // Atack
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float cooldown = 2f;
    private float lastAttack = 0f;
    [SerializeField] int weaponDamage = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        ProcessAttacks();
            
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessAttacks()
    {
        if (Time.time >= cooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                lastAttack = Time.time;
            }
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(moveX,moveY).normalized;

        if(moveDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            Flip();
        }


    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Attack()
    {
        animator.SetBool("IsAttacking", true);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,
            attackRange,
            enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            Debug.Log(enemy.gameObject.name);
            enemy.GetComponent<Entity>().TakeDamage(weaponDamage);
        }
    }

    public void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public override void Die()
    {
        animator.SetTrigger("Die");
    }
}
