using System;
using System.Collections;
using System.Collections.Generic;
using TopDown.Hotbar;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    [SerializeField]
    private HotbarController hotbarController;

    [SerializeField]
    private HealthBar healthBar;

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
        Debug.Log(scene.buildIndex);
        if (scene.buildIndex > 1)
        {
            hotbarController.gameObject.transform.parent.gameObject.SetActive(true);
            hotbarController.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            hotbarController.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
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
}
