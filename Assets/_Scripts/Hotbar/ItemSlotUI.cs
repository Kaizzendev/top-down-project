using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    Image itemImage;

    public bool isEmpty = true;

    public void SetData(Sprite sprite)
    {
        itemImage.sprite = sprite;
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
