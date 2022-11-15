using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;
    public event Action OnBack;
    Inventory inventory;

    int selectedItem = 0;

    private void Awake()
    {
        inventory = Inventory.instance;
    }

    private void Start()
    {
        UpdateItemList();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
            OnBack?.Invoke();
        }
    }

    void CloseInventory()
    {
       gameObject.SetActive(false);
    }

    void UpdateItemList()
    {
        foreach (Transform child in itemList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var itemSlot in inventory.Slots)
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);
        }
    }

}
