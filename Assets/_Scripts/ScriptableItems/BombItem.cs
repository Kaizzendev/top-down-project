using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Miscellaneous/Bomb")]
public class BombItem : Item
{
    public GameObject bombPrefab;

    public override void UseItem()
    {
        Instantiate(
            bombPrefab,
            GameManager.instance.player.transform.position,
            Quaternion.identity
        );
    }
}
