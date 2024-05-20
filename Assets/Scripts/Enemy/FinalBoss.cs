using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalBoss : MiniBoss
{
    
    private bool TeleportationAble = false;
    private Vector3 destinationPoint;
    public float tpLength = 10f;
    public float maxTpLength;

    protected override void Start()
    {
        Invoke(nameof(UnlockTeleportation), 4f);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentPoint = Random.Range(0, targetPoints.Length);
        walls = GameObject.FindGameObjectsWithTag("Wall").ToList();

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

    

    private void Teleportation()
    {
        if (TeleportationAble)
        {
            
            TeleportationAble = false;
            Invoke(nameof(UnlockTeleportation), 4f);
            rb.velocity = new Vector3(0, 0, 0);
            destinationPoint = new Vector3(transform.position.x-Random.Range(-tpLength, tpLength), transform.position.y-Random.Range(-tpLength, tpLength), 0);
            
            foreach (GameObject wall in walls)
                if (wall != null && Vector3.Distance(wall.transform.position, transform.position) < maxTpLength)
                    return;

            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, 3f);
            rb.velocity = new Vector3(0,0,0);

            ChangeElement();
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
                TakeDamage(buletElement, info);
        }


    }

    public override void Death()
    {
        rb.GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(gameObject);
        


    }


}
