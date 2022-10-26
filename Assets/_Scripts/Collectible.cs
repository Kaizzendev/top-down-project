using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, ICollectible
{
    public virtual void Collect()
    {
        Debug.Log("I am collecting this!");
    }
}
