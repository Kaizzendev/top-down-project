using TopDown.Player;
using TopDown.Hotbar;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isInventoryActive;
    public static GameManager insance;

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
        if (insance != null)
        {
            Destroy(gameObject);
            return;
        }
        insance = this;
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
}
