using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeableItem : Item
{
    [Header("PowerUp duration")]
    [SerializeField]
    protected float powerupDuration;
}
