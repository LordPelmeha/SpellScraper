﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public enum EvilAndKind { Evil, Kind };
public enum Magic { Fire, Water, Earth, Air };
public class Bullet : MagicHand
{
    public float speed;
    public float distance;

    [SerializeField] LayerMask WhatIsSolid;
    [SerializeField] bool enemyBullet;
    private RaycastHit2D hitInfo;
    public EvilAndKind emotion;
    public Magic element;
    private bool isCoounterMagic;
    public GameObject airHit;

    void Update()
    {
        hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                if (emotion == EvilAndKind.Evil)
                {
                    //сюда счётчик для концовки
                }
                if (emotion == EvilAndKind.Kind)
                {
                    //сюда счётчик для концовки
                }
                //hitInfo.collider.GetComponent<Enemy>().Death();
            }
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                if (GetComponent<Player>().health > 0)
                    GetComponent<Player>().health -= TakeDamage();
                else
                    hitInfo.collider.GetComponent<Player>().Death();
            }
            if (hitInfo.collider.CompareTag("Projectile"))
            {
                Counterattack(ref isCoounterMagic);
            }
            DestroyBullet();
            Destroy(airHit);


        }
        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    public void DestroyBullet()
    {
        //сюда анмиацию уничтожения пули

        Destroy(gameObject);
        airHit = Instantiate(airHit, transform.position, transform.rotation);
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
