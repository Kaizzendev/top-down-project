using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Recovery item")]
public class RecoveryItem : Item
{
    [Header("HP")]
    [SerializeField]
    int hpAmount;

    [SerializeField]
    bool restoreMaxHP;
}
