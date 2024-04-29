using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public enum EvilAndKind { Evil, Kind };
public enum Magic { Fire, Air, Water, Earth };
public class Bullet : MagicHand
{
    public float bulletSpead;
    [SerializeField] LayerMask WhatIsSolid;
    public bool enemyBullet;
    private RaycastHit2D hitInfo;
    [SerializeField] EvilAndKind emotion;
    [SerializeField] Magic element;
    private bool isCounterMagic;

    //public GameObject airHit;
    // Проверяем столкновение с объектом
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !enemyBullet)
        {
            collision.gameObject.GetComponent<Enemy>().EnemyTakeDamage(collision.gameObject.GetComponent<Enemy>().magicType - (int)element == 0 ? 0.5 : 1);
            if (collision.gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (emotion == EvilAndKind.Evil)
                {
                    countEnd--;
                }
                if (emotion == EvilAndKind.Kind)
                {
                    countEnd++;
                }
            }
        }
        if (collision.gameObject.CompareTag("Player") && enemyBullet)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && player.health > 0)
                player.health -= PlayerTakeDamage();
            DestroyBullet();
        }
        if (collision.gameObject.CompareTag("Projectile") && enemyBullet)
        {
            Counterattack(ref isCounterMagic, collision);
            if (isCounterMagic)
            {
                DestroyBullet();
                Destroy(collision.gameObject);
            }
            else
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(),collision.collider,true);
            }
        }
        if (!enemyBullet && (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy")))
        {
            DestroyBullet();
            //Destroy(airHit);
        }
        if (enemyBullet && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall")))
        {
            DestroyBullet();
        }
    }
    void Update()
    {
        transform.Translate(bulletSpead * Time.deltaTime * Vector3.up);
    }

    public void DestroyBullet()
    {
        //сюда анмиацию уничтожения пули

        Destroy(gameObject);
        //airHit = Instantiate(airHit, transform.position, transform.rotation);
    }
    private double PlayerTakeDamage()
    {
        return scrollMask - (int)element == 0 ? 0.5 : 1;
    }
    private void Counterattack(ref bool isCounterMagic, Collision2D collision)
    {
        Bullet obj = collision.gameObject.GetComponent<Bullet>();
       // Debug.Log($"{(int)obj.element} {(int)element}");
        isCounterMagic = (math.abs((int)obj.element - (int)element) % 2 == 0) &&(obj.element!=element);
    }
}
