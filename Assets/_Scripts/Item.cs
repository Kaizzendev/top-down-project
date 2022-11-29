using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item Description")]
    [SerializeField]
    string name;

    [SerializeField]
    string description;

    [Header("PowerUp duration")]
    [SerializeField]
    protected float powerupDuration;

    [SerializeField]
    Sprite icon;

    public string Name => name;
    public Sprite Icon => icon;

    public abstract void UseItem();
}
