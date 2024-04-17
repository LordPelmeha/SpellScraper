using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy : Enemy
{


    [SerializeField] Transform[] targetPoints;
    [SerializeField] int currentPoint;

    [Space]
    [Header("Timers")]
    [SerializeField] float maxWaitTime;
    [SerializeField] float minWaitTime;

    private int scale = 2;

    private float waitTime;
    private float waitTimeCounter;


    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        currentPoint = Random.Range(0, targetPoints.Length);

        waitTimeCounter = SetWaitTime();
        if (magicType == Magic.Fire)
            animType = "MoveEnemy";
        else if (magicType == Magic.Earth)
            animType = "EarthMove";
    }


    public override void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer())
        {

            MoveEnemy(player.position);

            moveSpeed *= scale;
            scale /= scale;

            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            playerPath.Enqueue(player.position);
            if (playerPath.Count > 100)
                playerPath.Dequeue();
        }
        //else if (distanceToPlayer <= detectionRange)
        //{
        //    ChasePlayer();
        //}
        else
        {
            playerPath.Clear();
            Patroling();
        }
    }

    private void Patroling()
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

    private float SetWaitTime()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        return waitTime;
    }



}
