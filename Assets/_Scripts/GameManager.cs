using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isInventoryActive;
    public static GameManager insance;

    MenuController menuController;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] Player player;

    public enum GameState
    {
        Normal,Menu,Pause,Inventory
    }

    public GameState gameState;

    private void Awake()
    {
        if(insance != null)
        {
            Destroy(gameObject);
            return;
        }
        insance = this;
        DontDestroyOnLoad(gameObject);

        menuController = GetComponent<MenuController>();

    }

    private void Start()
    {
        gameState = GameState.Normal;

        menuController.OnBack += () =>
        {
            gameState = GameState.Normal;
            player.state = Player.State.normal;
        };

        menuController.OnMenuSelected += OnMenuSelected;

        inventoryUI.OnBack += () =>
        {
            menuController.OpenMenu();
            gameState = GameState.Menu;
        };

    }

    public void Update()
    {
        switch (gameState)
        {
            case GameState.Normal:
                player.ProcessInputs();

            if(Input.GetKeyDown(KeyCode.E))
            {
                menuController.OpenMenu();
                gameState = GameState.Menu;
                player.state = Player.State.onMenus;
            }
        break;
        case GameState.Menu:

            menuController.HandleUpdate();
            break;
        case GameState.Inventory:

            inventoryUI.HandleUpdate();
            break;

        }

    }

    void OnMenuSelected(int selection)
    {
        if (selection == 0)
        {
            inventoryUI.gameObject.SetActive(true);
            gameState = GameState.Inventory;

        }
    }
}
