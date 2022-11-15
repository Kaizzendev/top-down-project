using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Collectible
{
    public override void Collect()
    {
       base.Collect();
       gameObject.SetActive(false);
    }
}
