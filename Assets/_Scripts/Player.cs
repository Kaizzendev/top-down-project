using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private bool facingRight = true;
    private Vector2 moveDirection;
    [SerializeField] private Animator animator;
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(moveX,moveY).normalized;

        if(moveDirection.x < 0 && facingRight)
        {
            Flip();
        }
        else if(moveDirection.x > 0 && !facingRight)
        {
            Flip();
        } 

    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        animator.SetFloat("speed", rb.velocity.x);
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}
