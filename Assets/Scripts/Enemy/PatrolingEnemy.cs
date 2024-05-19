using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy : Enemy
{

    [SerializeField] protected Transform[] targetPoints;
    [SerializeField] protected int currentPoint;
    public bool isPatrolingRandom;

    [Space]
    [Header("Timers")]
    [SerializeField] protected float maxWaitTime;
    [SerializeField] protected float minWaitTime;

    protected float chasingSpeed;
    protected float patrolingSpeed;

    protected float waitTime;
    protected float waitTimeCounter;


    protected override void Start()
    {
        chasingSpeed = moveSpeed * 2;
        patrolingSpeed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (isPatrolingRandom)
            currentPoint = Random.Range(0, targetPoints.Length);
        else
            currentPoint = 0;

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
                animType = "WindMove";
                break;
            case Magic.Water:
                animType = "WaterMove";
                break;
        }
    }


    protected override void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer() && (distanceToPlayer > stoppingRange))
        {

            ChasePlayer();

            moveSpeed = chasingSpeed;

            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        }
        else if ((distanceToPlayer <= detectionRange) && (distanceToPlayer <= stoppingRange) && CanSeePlayer())
        {          
            moveDelta = new Vector3(0f, 0f, 0f);
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0); 
        }
        else
        {
            moveSpeed = patrolingSpeed;
            Patroling();
        }
    }

    protected virtual void Patroling()
    {
        moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        animator.SetFloat(animType, Mathf.Abs(moveDelta.x));
        animator.SetFloat(animType, Mathf.Abs(moveDelta.y));
        animator.SetFloat(animType, Mathf.Abs(moveDelta.magnitude));

        if (transform.position == targetPoints[currentPoint].position)
        {
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);

            if (waitTimeCounter <= 0)
            {
                if(isPatrolingRandom) 
                    RandomizeCurrentPoint();
                else
                    IncreaseCurrentPoint();
                waitTimeCounter = SetWaitTime();
            }
            else
                waitTimeCounter -= Time.deltaTime;


        }

        Vector3 difference = targetPoints[currentPoint].position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotate);

        transform.position = Vector3.MoveTowards(transform.position, targetPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }

    private void IncreaseCurrentPoint()
    {
        currentPoint++;
        if (currentPoint >= targetPoints.Length)
        {
            currentPoint = 0;
        }
    }

    protected void RandomizeCurrentPoint()
    {
        currentPoint = Random.Range(0, targetPoints.Length);
        if (currentPoint >= targetPoints.Length)
        {
            currentPoint = 0;
        }
    }

    protected float SetWaitTime()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        return waitTime;
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

}
