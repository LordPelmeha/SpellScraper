using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy : Enemy
{

    private float waitTime;
    public float startWaitTime;

    public Transform[] spots;
    private int ransdomSpot;


    void Start()
    {

        waitTime = startWaitTime;
        ransdomSpot = Random.Range(0, spots.Length);
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, player.position) < detectionRange   /*CanSeePlayer()*/)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Time.time > lastShootTime + shootCooldown)
            {
                //Shoot();
                lastShootTime = Time.time;
            }
        }
        else
        {
            moveSpeed *= 2;
            transform.position = Vector2.MoveTowards(transform.position, spots[ransdomSpot].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, spots[ransdomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    ransdomSpot = Random.Range(0, spots.Length);
                    waitTime = startWaitTime;
                }
                else
                    waitTime -= Time.deltaTime;
            }
        }
    }
}
