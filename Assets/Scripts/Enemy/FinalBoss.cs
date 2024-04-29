using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MiniBoss
{
    
    private bool TeleportationAble = false;
    private Vector3 destinationPoint;
    public float tpLength = 10f;
    public float maxTpLength;
    private GameObject[] walls;

    protected override void Start()
    {
        Invoke(nameof(UnlockTeleportation), 1f);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentPoint = Random.Range(0, targetPoints.Length);
        walls = GameObject.FindGameObjectsWithTag("Wall");

        waitTimeCounter = SetWaitTime();
        switch (magicType)
        {
            case Magic.Fire:
                animType = "MoveEnemy";
                break;
            case Magic.Earth:
                animType = "EarthMove";
                break;
            case Magic.Air:
                animType = "AirEnemy";
                break;
            case Magic.Water:
                animType = "WaterEnemy";
                break;
        }
    }

    protected override void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer())
        {

            Patroling();
            Teleportation();
            transform.position = Vector3.MoveTowards(transform.position, targetPoints[currentPoint].position, moveSpeed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        }


    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !(collision.gameObject.GetComponent<Bullet>().enemyBullet))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.GetComponent<SpriteRenderer>().flipY = true;
            bullet.bulletSpead *= -5;
            bullet.enemyBullet = true;
        }
    }

    private void Teleportation()
    {
        if (TeleportationAble)
        {
            
            TeleportationAble = false;
            Invoke(nameof(UnlockTeleportation), 0.5f);
            rb.velocity = new Vector3(0, 0, 0);
            destinationPoint = new Vector3(transform.position.x-Random.Range(-tpLength, tpLength), transform.position.y-Random.Range(-tpLength, tpLength), 0);
            
            
            foreach (GameObject wall in walls)
            {
                if (Vector3.Distance(wall.transform.position, transform.position) < maxTpLength)
                {
                    Debug.Log("wall detected");
                    return;
                }

            }
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, 3f);
            rb.velocity = new Vector3(0,0,0);
        }


    }

    private void UnlockTeleportation()
    {
        TeleportationAble = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // ≈сли столкнулись со стеной, останавливаемс€
            rb.velocity = Vector3.zero;
        }
        else if (collision.collider.CompareTag("Projectile"))
        {

            Bullet info = collision.gameObject.GetComponent<Bullet>();
            Magic buletElement = info.element;

            if (!info.enemyBullet)
                TakeDamage(buletElement);
        }


    }

}
