using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    public event Action OnBack;

    public void OpenMenu()
    {
        menu.SetActive(true);
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
        menu.SetActive(false);
        Time.timeScale = 1;
        OnBack?.Invoke();
    }

    public void Resume()
    {
        CloseMenu();
    }

    public void Options()
    {
        // Open Options menu!
        Debug.Log("Options");
    }

    public void Quit()
    {
        // Quit Game
        Debug.Log("Quit");
    }
}
