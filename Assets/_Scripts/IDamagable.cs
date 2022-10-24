using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    int CurrentHealth { get; }
    int Health { get; }
    void TakeDamage(int Damage);
}
