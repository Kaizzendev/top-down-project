using TopDown.Hotbar;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, ICollectible
{
    [SerializeField]
    private Item item;

    private HotbarController hotbarController;

    private void Start()
    {
        hotbarController = FindObjectOfType<HotbarController>();
    }

    public virtual void Collect()
    {
        if (hotbarController != null)
        {
            hotbarController.CollectItem(item);
        }
    }
}
