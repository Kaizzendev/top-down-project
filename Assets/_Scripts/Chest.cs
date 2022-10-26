using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Sprite emptyChest;
    private bool collected;

    public override void Interact()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            // Hacer que salgan items
            //Instantiate();
        }
    }
}
