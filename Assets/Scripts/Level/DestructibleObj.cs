using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField] int health;
    private List<GameObject> miniBossWalls;
    private List<GameObject> bossWalls;
    public void Start()
    {
        miniBossWalls = GetComponent<MiniBoss>().walls;
        bossWalls = GetComponent<FinalBoss>().walls;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
            health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            miniBossWalls.Remove(gameObject);
            bossWalls.Remove(gameObject);
        }
            
    }

}
