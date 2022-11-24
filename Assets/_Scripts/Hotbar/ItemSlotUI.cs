using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    Image itemImage;

    private string name;

    public bool isEmpty = true;

    public void SetData(Item item)
    {
        itemImage.sprite = item.Icon;
        name = item.Name;
        itemImage.color = new Color(1, 1, 1, 1);
        isEmpty = false;
    }

    public void Use()
    {
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
        isEmpty = true;
        //Te cura, pone una bomba, te da dinero, te da velocidad, te da daño......
    }
}
