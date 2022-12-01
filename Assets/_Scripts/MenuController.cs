using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    public event Action<int> OnMenuSelected;
    public event Action OnBack;

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
            OnBack?.Invoke();
        }
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
