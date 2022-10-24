using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable 
{

    public int CurrentHealth
    { get; protected set; }

    public int Health
    { get; protected set; }

    private void Start()
    {
        CurrentHealth = Health;
    }

    public virtual void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
