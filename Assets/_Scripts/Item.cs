using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    string name;

    [SerializeField]
    string description;

    [SerializeField]
    Sprite icon;

    public string Name => name;
    public Sprite Icon => icon;
}
