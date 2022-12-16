using System.Collections;
using UnityEngine;

namespace TopDown.Player
{
    public class Player : Entity
    {
        public static Player Instance;

        public enum State
        {
            normal,
            rolling,
            attack,
            onMenus,
            death
        }

        [SerializeField]
        public State state;

        public HealthBar healthBar;

        public GameObject hint;

        public SpriteRenderer sprite;

        private Collider2D actualCollider;

        private Animator animator;

        private Rigidbody2D rb;

        [Header("Movement")]
        [SerializeField]
        private bool facingRight = true;

        public float moveSpeed;

        private bool invencible = false;

        private Vector2 moveDirection;

        [Header("Attack")]
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayers;
        public LayerMask playerLayer;
        public float cooldown = 0.5f;
        private float lastAttack = 0f;

        public int weaponDamage = 30;
        public float weaponKnockback = 50;

        [SerializeField]
        private Transform rotateWeaponPoint;

        [SerializeField]
        private BoxCollider2D weaponCollider;

        [SerializeField]
        private int stabDamage;

        [Header("Roll")]
        [SerializeField]
        private float rollSpeed;

        private float lastRoll;

        [SerializeField]
        private float rollCooldown;
        private Vector2 rollDir;

        [Header("Powerup")]
        public bool ispowerupInvencible = false;
        public bool isSpeedPowerUp = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            state = State.normal;
        }

        private void Start()
        {
            healthBar.SetHealth(currentHealth);
        }

        public void HandleUpdate()
        {
            ProcessInputs();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (
                collision.gameObject.GetComponent<Entity>() != null
                && !collision.gameObject.GetComponent<Entity>().CompareTag("Player")
            )
            {
                if (animator.GetBool("IsAttacking"))
                {
                    collision
                        .GetComponent<Entity>()
                        .TakeDamage(stabDamage, transform, weaponKnockback);
                }
                else
                {
                    collision
                        .GetComponent<Entity>()
                        .TakeDamage(weaponDamage, transform, weaponKnockback);
                }
            }

            if (collision.gameObject.GetComponent<Collectible>() != null)
            {
                collision.gameObject.GetComponent<Collectible>().Collect();
            }
            if (
                collision.gameObject.GetComponentInParent<Interactable>() != null
                && !collision.gameObject.GetComponentInParent<Interactable>().isInteracted
            )
            {
                if (
                    collision.gameObject.GetComponentInParent<Dialog>() != null
                    && collision.gameObject.GetComponentInParent<Dialog>().hasInteracted
                )
                {
                    actualCollider = collision;
                    return;
                }
                collision.gameObject.GetComponentInParent<Interactable>().Hint(true);
                actualCollider = collision;
            }
            if (
                collision.gameObject.GetComponentInParent<Interactable>() != null
                && collision.gameObject.GetComponentInParent<Interactable>().isInteracted
                && collision.gameObject.GetComponentInParent<Interactable>().CompareTag("Portal")
                && !collision.gameObject.GetComponent<Interactable>().CompareTag("Hint")
            )
            {
                GameManager.instance.NextLevel();
                rb.position = Vector2.zero;
            }
            if (
                collision.gameObject.GetComponentInParent<Interactable>() != null
                && collision.gameObject.GetComponentInParent<Interactable>().isInteracted
                && collision.gameObject
                    .GetComponentInParent<Interactable>()
                    .CompareTag("PortalCredits")
                && !collision.gameObject.GetComponent<Interactable>().CompareTag("Hint")
            )
            {
                GameManager.instance.GoCredits();
                state = State.onMenus;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponentInParent<Interactable>() != null)
            {
                collision.gameObject.GetComponentInParent<Interactable>().Hint(false);
                actualCollider = null;
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

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - rotateWeaponPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (!facingRight)
            {
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
            }
            rotateWeaponPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if ((Time.time - lastAttack) > cooldown)
                        {
                            lastAttack = Time.time;
                            state = State.attack;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (
                            (Time.time - lastRoll) > rollCooldown
                            && moveDirection != Vector2.zero
                            && !isSpeedPowerUp
                        )
                        {
                            rollDir = moveDirection;
                            rollSpeed = 12f;
                            lastRoll = Time.time;
                            animator.SetTrigger("Roll");
                            state = State.rolling;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.E) && actualCollider != null)
                    {
                        actualCollider.gameObject.GetComponentInParent<Interactable>().Interact();
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
                case State.death:
                    Debug.Log(name + " is death :/");
                    break;
            }
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

            //Collider2D[] enemies = Physics2D.OverlapCircleAll(
            //    rotateWeaponPoint.GetChild(0).transform.position,
            //    attackRange,
            //    enemyLayers
            //);
            //foreach (Collider2D enemy in enemies)
            //{
            //    Debug.Log(enemy.gameObject.name);
            //    enemy.GetComponent<Entity>().TakeDamage(weaponDamage, transform, weaponKnockback);
            //}
        }

        public void EndAttack()
        {
            state = State.normal;
            animator.SetBool("IsAttacking", false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(rotateWeaponPoint.GetChild(0).transform.position, attackRange);
        }

        public override void TakeDamage(int Damage, Transform transform, float weaponKnockback)
        {
            if (invencible)
            {
                return;
            }

            base.TakeDamage(Damage, transform, weaponKnockback);
            healthBar.SetHealth(currentHealth);
            if (state != State.death)
            {
                animator.SetTrigger("Hurt");
                StartCoroutine(Hit());
            }
        }

        public void SetInvencible()
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
        }

        public void SetMortal()
        {
            if (!ispowerupInvencible)
            {
                Physics2D.IgnoreLayerCollision(6, 7, false);
            }
        }

        private IEnumerator Hit()
        {
            invencible = true;
            yield return new WaitForSeconds(1);
            invencible = false;
        }

        public override void Die()
        {
            state = State.death;
            animator.SetTrigger("Die");
            rb.velocity = Vector3.zero;
            GetComponent<Collider2D>().enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(2);
            GetComponent<Collider2D>().enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            state = State.normal;
            currentHealth = health;
            if (GameManager.instance.checkpoint != null)
            {
                rb.position = GameManager.instance.checkpoint;
                animator.SetTrigger("Respawn");
            }
            else
            {
                rb.position = Vector3.zero;
            }
        }
    }
}
