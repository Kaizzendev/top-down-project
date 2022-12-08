using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Sprite emptyChest;

    [SerializeField]
    private int numberOfitemsToDrop;

    [SerializeField]
    private List<GameObject> items;

    public override void Interact()
    {
        if (!isInteracted)
        {
            Hint(false);
            isInteracted = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            foreach (var item in ItemsToDrop())
            {
                Instantiate(item);
            }
        }
    }

    private List<GameObject> ItemsToDrop()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 1; i <= numberOfitemsToDrop; i++)
        {
            int ranNum = Random.Range(0, items.Count);
            list.Add(items[ranNum]);
        }
        return list;
    }
}
