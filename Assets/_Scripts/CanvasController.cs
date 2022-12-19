using TopDown.Hotbar;
using TopDown.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    [SerializeField]
    private HotbarController hotbarController;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject credits;

    [SerializeField]
    private GameObject controls;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 1)
        {
            mainMenu.SetActive(false);
            hotbarController.gameObject.transform.parent.gameObject.SetActive(true);
            hotbarController.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            hotbarController.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
        }
        if (scene.name.Equals("Main"))
        {
            transform.GetChild(2).gameObject.SetActive(false);
            Player.Instance.state = Player.State.onMenus;
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(true);
            Player.Instance.state = Player.State.normal;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowControls()
    {
        controls.SetActive(true);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void GoBack()
    {
        controls.SetActive(false);
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
