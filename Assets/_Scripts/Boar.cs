using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Entity
{
    Animator anim;
    [SerializeField] private int health;
    [SerializeField] private int currentHealth;
    private void Awake()
    {
        Health = health;
        currentHealth = health;
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(int Damage)
    {
        base.TakeDamage(Damage);
        currentHealth = CurrentHealth;
        anim.SetTrigger("Hurt");
    }
    public override void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
    }
}
