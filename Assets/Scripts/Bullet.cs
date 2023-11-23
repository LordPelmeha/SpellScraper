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
    private bool isCoounterMagic;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем столкновение с объектом
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemy");
            if (emotion == EvilAndKind.Evil)
            {
                // сюда счётчик для концовки
            }
            if (emotion == EvilAndKind.Kind)
            {
                // сюда счётчик для концовки
            }
             //collision.gameObject.GetComponent<Enemy>().Death();
        }
        if (collision.gameObject.CompareTag("Player") && enemyBullet)
        {
            if (GetComponent<Player>().health > 0)
                GetComponent<Player>().health -= TakeDamage();
            else
                collision.gameObject.GetComponent<Player>().Death();
            DestroyBullet();
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Counterattack(ref isCoounterMagic);
            if (isCoounterMagic)
                Destroy(collision.gameObject.GetComponent<Bullet>());

        }
        if (collision.gameObject.layer == 8  || collision.gameObject.layer == 10)
        {
            DestroyBullet();
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
    }
    private double TakeDamage()
    {
        return scrollMask - (int)element == 0 ? 0.5 : 1;
    }
    private void Counterattack(ref bool isCoounterMagic)
    {
        Bullet obj = hitInfo.collider.GetComponent<Bullet>();
        isCoounterMagic = math.abs((int)obj.element - (int)element) % 2 == 0;
        Destroy(obj);
    }
}
