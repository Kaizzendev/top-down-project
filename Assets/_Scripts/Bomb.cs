using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject go;

    public void Explosion()
    {
        if (go != null)
        {
            Destroy(go);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Debris"))
        {
            go = collision.transform.parent.gameObject;
        }
    }
}
