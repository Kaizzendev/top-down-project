using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, ICollectible
{
    [SerializeField]
    private Item item;

    public virtual void Collect()
    {
        Debug.Log("I am collecting this!");
    }
}
