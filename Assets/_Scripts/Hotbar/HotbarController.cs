using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("Hay " + itemSlotUIs.Count + " slots");
        }

        public void CollectItem(Item item)
        {
            for (int i = 0; i < size; i++)
            {
                if (itemSlotUIs[0].isEmpty)
                {
                    itemSlotUIs[i].SetData(item.Icon);
                }
            }
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
    }
}
