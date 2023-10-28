using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private float distance = float.PositiveInfinity;

    public LayerMask WhatIsSolid;
    private RaycastHit2D hitInfo;
    [SerializeField] bool enemyBullet;


    void Update()
    {
        hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
                hitInfo.collider.GetComponent<Enemy>().Death();

            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
                hitInfo.collider.GetComponent<Player>().Death();
            Destroy(gameObject);
        }

        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    public void DestroyBullet()
    {

    }
}
