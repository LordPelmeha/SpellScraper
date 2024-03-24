using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField] int health;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
            health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
