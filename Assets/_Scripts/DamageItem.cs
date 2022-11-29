using TopDown.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Damage item")]
public class DamageItem : Item
{
    [Header("Damage")]
    [SerializeField]
    private int damageBoost;

    [SerializeField]
    private int knockbackBoost;

    [Header("Power up duration")]
    [SerializeField]
    private float powerupDuration;

    public override void UseItem()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            GameManager.instance.PowerwUpTImer(powerupDuration, damageBoost, knockbackBoost);
        }
    }
}
