using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EvilAndKind { Evil, Kind };
public enum Magic { Fire, Water, Earth, Air };
public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;

    [SerializeField] LayerMask WhatIsSolid;
    [SerializeField] bool enemyBullet;
    private RaycastHit2D hitInfo;
    public EvilAndKind emotion;
    public Magic element;

    void Update()
    {
        hitInfo = Physics2D.Raycast(transform.position, transform.up, 0.01f, WhatIsSolid);
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
                    GetComponent<Player>().health -= 1;
                else
                    hitInfo.collider.GetComponent<Player>().Death();
            }

            DestroyBullet();
        }
        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    public void DestroyBullet()
    {
        //сюда анмиацию уничтожения пули
        Destroy(gameObject);
    }
    private double TakeDamage()
    {
        double dmg = 1;
        return dmg-0.5*0;
    }
    private bool Counterattack()
    {
       // GameObject otherObj= 
    }
}
