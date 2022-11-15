using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void TakeDamage(int Damage, Transform transform, float power);

    void KnockBack(Transform transform, float power);
}
