using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Collectible
{
    public override void Collect()
    {
        base.Collect();
        gameObject.SetActive(false);
    }
}

