using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField] int health;
    private List<GameObject> miniBossWalls;
    private bool isMiniBossOnLevel;
    private bool isFinalBossLevel;
    private List<GameObject> bossWalls;
    [SerializeField] Sprite broken;
    SpriteRenderer sprite;
    public void Start()
    {
        if (isMiniBossOnLevel)
            miniBossWalls = GameObject.FindWithTag("Enemy").GetComponent<MiniBoss>().walls;
        else if (isFinalBossLevel)
            bossWalls = GameObject.FindWithTag("Enemy").GetComponent<FinalBoss>().walls;
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
        {
            Destroy(gameObject);
            if (isMiniBossOnLevel)
                miniBossWalls.Remove(gameObject);
            if (isFinalBossLevel)
                bossWalls.Remove(gameObject);
        }

    }

}
