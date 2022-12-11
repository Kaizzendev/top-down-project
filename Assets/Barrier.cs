using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Campfire checkpoint;

    private void Update()
    {
        if (checkpoint.isInteracted)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
