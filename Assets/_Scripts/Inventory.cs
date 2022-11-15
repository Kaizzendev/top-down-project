using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] List<ItemSlot> slots;
    public List<ItemSlot> Slots => slots;

    public void AddItem(Item collectedItem)
    {
        // TODO recoger items y añadirlos al inventario
    }

}
[Serializable]
public class ItemSlot
{
    [SerializeField] Item item;
    [SerializeField] int count;

    public Item Item => item;
    public int Count => count;
}
