using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagableItem
{
    int DamageBoost { get; set; }
    int KnockbackBoost { get; set; }
}
