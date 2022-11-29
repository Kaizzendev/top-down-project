using System.Collections;
using UnityEngine;

namespace TopDown.Player
{
    public class Player : Entity
    {
        public enum State
        {
            normal,
            rolling,
            attack,
            onMenus
        }

        [SerializeField]
        public State state;

        // Movement
        private Rigidbody2D rb;

        [SerializeField]
        private float moveSpeed;
        private bool facingRight = true;
        private Vector2 moveDirection;

        // Animation
        private Animator animator;

        // Atack
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayers;
        public LayerMask playerLayer;
        public float cooldown = 0.5f;
        private float lastAttack = 0f;

        public int weaponDamage = 30;
        public float weaponKnockback = 50;
        private bool isAttacking;

        private bool invencible = false;

        //Roll
        private float lastRoll;

        [SerializeField]
        private float rollSpeed;

        [SerializeField]
        private float rollCooldown;
        private Vector2 rollDir;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            state = State.normal;
        }

        public void HandleUpdate()
        {
            ProcessInputs();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Collectible>() != null)
            {
                collision.gameObject.GetComponent<Collectible>().Collect();
            }
        }

        public void FixedUpdate()
        {
            switch (state)
            {
                case State.normal:
                    Move();
                    break;
                case State.rolling:
                    Roll();
                    break;
                case State.attack:
                    Attack();
                    break;
                case State.onMenus:
                    rb.velocity = Vector3.zero;
                    break;
            }
        }

        private void Roll()
        {
            rb.velocity = rollDir * rollSpeed;
        }

        public void ProcessInputs()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            switch (state)
            {
                case State.normal:
                    moveDirection = new Vector2(moveX, moveY).normalized;

                    if (moveDirection.x > 0 && !facingRight)
                    {
                        Flip();
                    }
                    else if (moveDirection.x < 0 && facingRight)
                    {
                        Flip();
                    }

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        var interactable = FindNearestGameObject();
                        float distance = Vector3.Distance(
                            transform.position,
                            interactable.transform.position
                        );
                        if (interactable != null && distance < 1)
                        {
                            interactable.Interact();
                            //state = State.onMenus;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if ((Time.time - lastAttack) > cooldown)
                        {
                            lastAttack = Time.time;
                            state = State.attack;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        if ((Time.time - lastRoll) > rollCooldown && moveDirection != Vector2.zero)
                        {
                            rollDir = moveDirection;
                            rollSpeed = 12f;
                            lastRoll = Time.time;
                            animator.SetTrigger("Roll");
                            state = State.rolling;
                        }
                    }
                    break;
                case State.rolling:
                    float rollSpeedDropMultiplier = 3f;
                    rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                    float rollSpeedMinimun = 4f;
                    if (rollSpeed < rollSpeedMinimun)
                    {
                        state = State.normal;
                    }
                    break;
                case State.attack:
                    Debug.Log(name + " is attacking!");
                    break;
            }
        }

        private Interactable FindNearestGameObject()
        {
            float distanceToClosestInteractable = Mathf.Infinity;
            Interactable[] allInteractables = GameObject.FindObjectsOfType<Interactable>();
            Interactable closestInteractable = null;

            foreach (Interactable interactable in allInteractables)
            {
                float distanceToInteractable = (
                    interactable.transform.position - this.transform.position
                ).sqrMagnitude;
                if (distanceToInteractable < distanceToClosestInteractable)
                {
                    distanceToClosestInteractable = distanceToInteractable;
                    closestInteractable = interactable;
                }
            }
            return closestInteractable;
        }

        private void Move()
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            animator.SetFloat("speed", Mathf.Abs(Mathf.Abs(rb.velocity.magnitude)));
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
            rb.velocity = Vector3.zero;
            animator.SetBool("IsAttacking", true);

            Collider2D[] enemies = Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayers
            );
            foreach (Collider2D enemy in enemies)
            {
                Debug.Log(enemy.gameObject.name);
                enemy.GetComponent<Entity>().TakeDamage(weaponDamage, transform, weaponKnockback);
            }
        }

        public void EndAttack()
        {
            state = State.normal;
            animator.SetBool("IsAttacking", false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public override void TakeDamage(int Damage, Transform transform, float weaponKnockback)
        {
            if (invencible)
                return;

            base.TakeDamage(Damage, transform, weaponKnockback);
            animator.SetTrigger("Hurt");
            StartCoroutine(Hit());
        }

        public void SetInvencible()
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
        }

        public void SetMortal()
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }

        private IEnumerator Hit()
        {
            invencible = true;
            yield return new WaitForSeconds(1);
            invencible = false;
        }

        public override void Die()
        {
            animator.SetTrigger("Die");
        }
    }
}
