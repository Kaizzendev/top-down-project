using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

namespace TopDown.Hotbar
{
    public class HotbarController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private ItemSlotUI itemSlotUIPrefab;

        [SerializeField]
        private List<ItemSlotUI> itemSlotUIs = new List<ItemSlotUI>();

        [SerializeField]
        private int maxSize;

        [SerializeField]
        private int size;

        private void Start()
        {
            Initialize(size);
        }

        private void Initialize(int size)
        {
            for (int i = 0; i < size; i++)
            {
                ItemSlotUI itemSlotUI = Instantiate(
                    itemSlotUIPrefab,
                    Vector3.zero,
                    Quaternion.identity
                );
                itemSlotUI.transform.SetParent(rectTransform, false);
                itemSlotUIs.Add(itemSlotUI);
            }
        }

        public void CollectItem(Item item, Collectible collectible)
        {
            for (int i = 0; i < size; i++)
            {
                if (itemSlotUIs[i].isEmpty && !IsThisItem(item))
                {
                    itemSlotUIs[i].SetData(item);
                    collectible.gameObject.SetActive(false);
                    return;
                }
            }
        }

        public bool IsThisItem(Item itemTypeToSearch)
        {
            for (int i = 0; i < itemSlotUIs.Count; i++)
            {
                if (itemSlotUIs[i].itemName == itemTypeToSearch.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public void HandleInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !itemSlotUIs[0].isEmpty)
            {
                itemSlotUIs[0].Use();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && !itemSlotUIs[1].isEmpty)
            {
                itemSlotUIs[1].Use();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && !itemSlotUIs[2].isEmpty)
            {
                itemSlotUIs[2].Use();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !itemSlotUIs[3].isEmpty)
            {
                itemSlotUIs[3].Use();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && !itemSlotUIs[4].isEmpty)
            {
                itemSlotUIs[4].Use();
            }
        }

        public void AddItemSLotUI()
        {
            ItemSlotUI itemSlotUI = Instantiate(
                itemSlotUIPrefab,
                Vector3.zero,
                Quaternion.identity
            );
            itemSlotUI.transform.SetParent(rectTransform, false);
            itemSlotUIs.Add(itemSlotUI);
        }
    }
}
