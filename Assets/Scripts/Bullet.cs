using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public enum EvilAndKind { Evil, Kind };
public enum Magic { Fire, Water, Earth, Air };
public class Bullet : MagicHand
{
    [SerializeField] float speed;
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
            Debug.Log(collision.gameObject.GetComponent<Enemy>().health);
            if (collision.gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (emotion == EvilAndKind.Evil)
                {
                    // сюда счётчик для концовки
                }
                if (emotion == EvilAndKind.Kind)
                {
                    // сюда счётчик для концовки
                }
            }
        }
        if (collision.gameObject.CompareTag("Player") && enemyBullet)
        {
            
            if (GetComponent<Player>().health > 0)
                GetComponent<Player>().health -= PlayerTakeDamage();
            else
                collision.gameObject.GetComponent<Player>().Death();
            DestroyBullet();
        }
        if (collision.gameObject.CompareTag("Projectile") && enemyBullet)
        {
            Counterattack(ref isCounterMagic);
            if (isCounterMagic)
                DestroyBullet();
        }
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10 )
        {
            DestroyBullet();
            //Destroy(airHit);
        }
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
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
    private void Counterattack(ref bool isCounterMagic)
    {
        Bullet obj = hitInfo.collider.GetComponent<Bullet>();
        isCounterMagic = math.abs((int)obj.element - (int)element) % 2 == 0;
        Destroy(obj);
    }
}
