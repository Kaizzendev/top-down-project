using System.Collections;
using System.Collections.Generic;
using TopDown.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Recovery item")]
public class RecoveryItem : Item
{
    [Header("HP")]
    [SerializeField]
    int hpAmount;

    [SerializeField]
    bool restoreMaxHP;

    public override void UseItem()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            if (player.currentHealth + hpAmount > player.health)
            {
                player.currentHealth = player.health;
            }
            else
            {
                player.currentHealth += hpAmount;
            }
        }
    }
}
