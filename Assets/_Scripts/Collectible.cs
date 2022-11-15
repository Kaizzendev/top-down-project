using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, ICollectible
{
    [SerializeField] private Item item;

    public virtual void Collect()
    {
        Inventory.instance.AddItem(item);
        Debug.Log("I am collecting this!");
    }
}
