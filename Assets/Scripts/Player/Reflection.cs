using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Windows;

public class Reflection : MonoBehaviour
{
    [SerializeField] float startTimer;
    private float currentTimer;
    public void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log(UnityEngine.Input.GetKeyDown(KeyCode.E));
        if (collision.gameObject.CompareTag("Projectile")  && collision.gameObject.GetComponent<Bullet>().enemyBullet 
            && UnityEngine.Input.GetKey(KeyCode.Space) && currentTimer <= 0)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.GetComponent<SpriteRenderer>().flipY = true ;
            bullet.bulletSpead *= -2;
            bullet.enemyBullet = false;
            currentTimer = startTimer;
        }   
    }
    private void Update()
    {
        currentTimer-=Time.deltaTime;
    }
}
