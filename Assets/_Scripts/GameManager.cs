using TopDown.Player;
using TopDown.Hotbar;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    MenuController menuController;

    [SerializeField]
    Player player;

    [SerializeField]
    HotbarController hotbarController;

    public enum GameState
    {
        Normal,
        Menu,
        Dialog
    }

    public GameState gameState;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        menuController = GetComponent<MenuController>();
        gameState = GameState.Normal;

        menuController.OnBack += () =>
        {
            gameState = GameState.Normal;
            player.state = Player.State.normal;
        };

        menuController.OnMenuSelected += OnMenuSelected;
    }

    public void Update()
    {
        switch (gameState)
        {
            case GameState.Normal:
                player.HandleUpdate();
                hotbarController.HandleInputs();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    menuController.OpenMenu();
                    gameState = GameState.Menu;
                    player.state = Player.State.onMenus;
                }
                break;
            case GameState.Menu:
                menuController.HandleUpdate();
                break;
        }
    }

    void OnMenuSelected(int selection)
    {
        switch (selection)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public void PowerwUpTImer(float duration, int damageBoost, int knockbackBoost, float speedBoost)
    {
        StartCoroutine(PowerUpTimer(duration, damageBoost, knockbackBoost, speedBoost));
    }

    public void PowerwUpTImer(float duration, int damageBoost, int knockbackBoost)
    {
        StartCoroutine(PowerUpTimer(duration, damageBoost, knockbackBoost));
    }

    public void PowerwUpTImer(float duration, float speedBoost, float rollBoostSpeed)
    {
        StartCoroutine(PowerUpTimer(duration, speedBoost));
    }

    private IEnumerator PowerUpTimer(float duration, float speedBoost)
    {
        player.moveSpeed += speedBoost;
        player.isSpeedPowerUp = true;
        yield return new WaitForSeconds(duration);
        player.isSpeedPowerUp = false;
        player.moveSpeed -= speedBoost;
    }

    private IEnumerator PowerUpTimer(float duration, int damageBoost, int knockbackBoost)
    {
        player.weaponDamage += damageBoost;
        player.weaponKnockback += knockbackBoost;
        yield return new WaitForSeconds(duration);
        player.weaponDamage -= damageBoost;
        player.weaponKnockback -= knockbackBoost;
    }

    private IEnumerator PowerUpTimer(
        float duration,
        int damageBoost,
        int knockbackBoost,
        float speedBoost
    )
    {
        player.currentHealth = player.health;
        player.sprite.color = Color.yellow;
        player.weaponDamage += damageBoost;
        player.weaponKnockback += knockbackBoost;
        player.moveSpeed += speedBoost;
        player.ispowerupInvencible = true;
        player.SetInvencible();
        player.isSpeedPowerUp = true;
        yield return new WaitForSeconds(duration);
        player.isSpeedPowerUp = false;
        player.ispowerupInvencible = false;
        player.SetMortal();
        player.moveSpeed -= speedBoost;
        player.weaponDamage -= damageBoost;
        player.weaponKnockback -= knockbackBoost;
        player.sprite.color = Color.white;
    }
}
