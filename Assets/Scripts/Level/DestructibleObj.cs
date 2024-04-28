using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Sprite broken;
    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health--;
            sprite.sprite = broken;
        }

        if (health <= 0)
            Destroy(gameObject);
    }
}
