using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item Description")]
    [SerializeField]
    string itemName;

    [SerializeField]
    string description;

    [SerializeField]
    Sprite icon;

    public string Name => itemName;
    public Sprite Icon => icon;

    public abstract void UseItem();
}
