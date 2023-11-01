using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance = float.PositiveInfinity;

    public LayerMask WhatIsSolid;
    private RaycastHit2D hitInfo;
    [SerializeField] bool enemyBullet;

    void Update()
    {
        hitInfo = Physics2D.Raycast(transform.position, transform.up, 0.01f, WhatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
                hitInfo.collider.GetComponent<Enemy>().Death();

            //if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            //    hitInfo.collider.GetComponent<Player>().Death();
            Destroy(gameObject);
        }
        transform.Translate(speed * Time.deltaTime * Vector3.up);

    }

    public void DestroyBullet()
    {

    }
    
}
