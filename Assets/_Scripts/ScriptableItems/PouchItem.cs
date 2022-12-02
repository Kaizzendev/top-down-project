using System.Collections;
using System.Collections.Generic;
using TopDown.Hotbar;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Miscellaneous/Pouch")]
public class PouchItem : Item
{
    public override void UseItem()
    {
        HotbarController hotbarController = FindObjectOfType<HotbarController>();
        if (hotbarController == null)
        {
            return;
        }
        hotbarController.AddItemSLotUI();
    }
}
