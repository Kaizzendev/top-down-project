using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public List<GameObject> prefabs;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
        SaveGO();
    }

    private void SaveGO()
    {
        foreach (GameObject go in prefabs)
        {
            DontDestroyOnLoad(go);
        }
    }
}
