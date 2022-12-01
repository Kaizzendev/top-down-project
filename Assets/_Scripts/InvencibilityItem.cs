using TopDown.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Invencibility item")]
public class InvencibilityItem : MovilityItem, IDamagableItem
{
    [Header("Damage")]
    [SerializeField]
    private int damageBoost;

    [SerializeField]
    private int knockbackBoost;
    public int DamageBoost
    {
        get { return damageBoost; }
        set { damageBoost = value; }
    }
    public int KnockbackBoost
    {
        get { return knockbackBoost; }
        set { knockbackBoost = value; }
    }

    public override void UseItem()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            GameManager.instance.PowerwUpTImer(
                powerupDuration,
                damageBoost,
                knockbackBoost,
                boostSpeed
            );
        }
    }
}
