using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    Image itemImage;

    public string itemName;

    public bool isEmpty = true;

    private Item item;

    public void SetData(Item item)
    {
        itemImage.sprite = item.Icon;
        itemName = item.Name;
        itemImage.color = new Color(1, 1, 1, 1);
        isEmpty = false;
        this.item = item;
    }

    public void Use()
    {
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
        isEmpty = true;
        itemName = "noItem";
        //Te cura, pone una bomba, te da dinero, te da velocidad, te da daño......
        item.UseItem();
    }
}
