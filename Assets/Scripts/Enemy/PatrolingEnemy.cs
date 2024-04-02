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
    }


    public override void Update()
    {
        if ((Vector3.Distance(transform.position, player.position) < detectionRange) && CanSeePlayer())
        {

            moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
            if (moveDelta.magnitude > 1f)
                moveDelta.Normalize();
            animator.SetFloat("MoveEnemy", Mathf.Abs(moveDelta.x));
            animator.SetFloat("MoveEnemy", Mathf.Abs(moveDelta.y));
            animator.SetFloat("MoveEnemy", Mathf.Abs(moveDelta.magnitude));    

            Vector3 difference = player.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotate);

            moveSpeed *= scale;
            scale /= scale;

            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Patroling();
        }
    }

    private void Patroling()
    {
        moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        animator.SetFloat("MoveEnemy", Mathf.Abs(moveDelta.x));
        animator.SetFloat("MoveEnemy", Mathf.Abs(moveDelta.y));
        animator.SetFloat("MoveEnemy", Mathf.Abs(moveDelta.magnitude));

        if (transform.position == targetPoints[currentPoint].position)
        {
            animator.SetFloat("MoveEnemy", 0);
            animator.SetFloat("MoveEnemy", 0);
            animator.SetFloat("MoveEnemy", 0);

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
