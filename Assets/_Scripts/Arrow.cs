using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Entity>() != null)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(damage, transform, 2f);
        }
            Destroy(gameObject);
    }
}
