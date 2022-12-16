using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public event Action OnBack;

    private void Start()
    {
        GameManager.instance.menuController = this;
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        OnBack?.Invoke();
    }

    public void Resume()
    {
        CloseMenu();
    }

    public void MainMenu()
    {
        GameManager.instance.GoMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
