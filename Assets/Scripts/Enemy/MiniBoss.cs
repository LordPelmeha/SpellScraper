using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBoss : PatrolingEnemy
{
    public bool isFinalMiniBoss;
    public Magic[] elements;
    bool DashAble = false;
    public GameObject finalBoss;
    public int curMagicType;


    Vector3 dashDirection;
    readonly float _speedRotatee = 30f;
    public float dashSpeed;
    public float maxDashLength ;
    public List<GameObject> walls;

    protected override void Start()
    {
        
        Invoke(nameof(UnlockDash), 4f);
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
        //Debug.Log(walls.Count);
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer())
        {
            Patroling();
            Dash();
            transform.position = Vector3.MoveTowards(transform.position, targetPoints[currentPoint].position, moveSpeed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        }
    }

    

    


    protected override void Patroling()
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
                RandomizeCurrentPoint();
            else
                waitTimeCounter -= Time.deltaTime;


        }

        Vector3 difference = player.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotatee);

        transform.position = Vector3.MoveTowards(transform.position, targetPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }




    private void Dash()
    {
        
        if (DashAble)
        {
           
            DashAble = false;
            Invoke(nameof(UnlockDash), 4f);
            rb.velocity = new Vector3(0, 0, 0);
            dashDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Vector3 move = dashDirection * dashSpeed;

            foreach (GameObject wall in walls)
                if (wall.gameObject!=null && Vector3.Distance(wall.transform.position, transform.position)< maxDashLength)
                    return;
    
            rb.velocity = move;
            StartCoroutine(DashCoroutine());

        }
    }


    protected virtual IEnumerator DashCoroutine()
    {        
        yield return new WaitForSeconds(0.3f);
        StopCoroutine(DashCoroutine());

        rb.velocity = new Vector3(0, 0, 0);
        ChangeElement();
    }

    protected virtual void UnlockDash()
    {
        DashAble = true;
    }

    protected virtual void ChangeElement()
    {
        curMagicType =Random.Range(0,elements.Length);
        magicType = elements[curMagicType];
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
                TakeDamage(buletElement,info);
        }


    }


    public override void Death()
    {
        base.Death();

        if(isFinalMiniBoss) 
        {
            finalBoss.transform.position = transform.position;
        }
    }

}

