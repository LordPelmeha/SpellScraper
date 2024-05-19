using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public enum EvilAndKind { Evil, Kind };
public enum Magic { Fire, Earth, Water, Air };
public class Bullet : MagicHand
{
    public Rigidbody2D bulletrb;
    public float bulletSpead;
    [SerializeField] LayerMask WhatIsSolid;
    protected LayerMask excludeLayers;
    public bool enemyBullet;
    [SerializeField] public EvilAndKind emotion;
    [SerializeField] public Magic element;
    private bool isCounterMagic;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !enemyBullet)
        {

            //if (collision.gameObject.GetComponent<Enemy>().health <= 0)
            //{
                
                
            //}
            
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
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
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

    }
    private double PlayerTakeDamage()
    {
        return scrollMask - (int)element == 0 ? 0.5 : 1;
    }
    private void Counterattack(ref bool isCounterMagic, Collision2D collision)
    {
        Bullet obj = collision.gameObject.GetComponent<Bullet>();
        // Debug.Log($"{(int)obj.element} {(int)element}");
        isCounterMagic = (math.abs((int)obj.element - (int)element) % 2 == 0) && (obj.element != element);
    }
}
