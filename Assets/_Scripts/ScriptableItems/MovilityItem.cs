using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Movility item")]
public class MovilityItem : TimeableItem
{
    [Header("Speed")]
    [SerializeField]
    protected float boostSpeed;

    [SerializeField]
    protected float rollBoostSpeed;

    public override void UseItem()
    {
        GameManager.instance.PowerwUpTImer(powerupDuration, boostSpeed, rollBoostSpeed);
    }
}
