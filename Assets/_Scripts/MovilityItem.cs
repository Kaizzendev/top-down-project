using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Movility item")]
public class MovilityItem : Item
{
    [SerializeField]
    private float boostSpeed;

    public override void UseItem()
    {
        GameManager.instance.PowerwUpTImer(powerupDuration, boostSpeed);
    }
}
