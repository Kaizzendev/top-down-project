using TopDown.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Damage item")]
public class DamageItem : Item
{
    [Header("Damage")]
    [SerializeField]
    private int damageBoost;

    [Header("Power up duration")]
    [SerializeField]
    private float powerupDuration;

    public override void UseItem()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.weaponDamage += damageBoost;
            GameManager.instance.PowerwUpTImer(powerupDuration, damageBoost);
            player.weaponDamage -= damageBoost;
        }
    }
}
