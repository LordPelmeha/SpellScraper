using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReflection : MonoBehaviour
{
    [SerializeField] float startTimer;
    private float currentTimer;

    private void Update()
    {
        currentTimer -= Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if (currentTimer <= 0 && collision.gameObject.CompareTag("Projectile") && !(collision.gameObject.GetComponent<Bullet>().enemyBullet))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.GetComponent<SpriteRenderer>().flipY = true;
            bullet.bulletSpead *= -5;
            bullet.enemyBullet = true;
            currentTimer = startTimer;
        }

    }

}
