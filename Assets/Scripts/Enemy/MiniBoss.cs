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
                DeathName = "Death_EarthFire";
                break;
            case Magic.Air:
                DeathName = "Death_FireWind";
                break;
            case Magic.Water:
                DeathName = "WaterWind_Death";
                break;
        }
    }


    protected override void Update()
    {
        if (isDead) return;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer())
        {
            Patroling();
            Dash();
            transform.position = Vector3.MoveTowards(transform.position, targetPoints[currentPoint].position, moveSpeed * Time.deltaTime);
        }
    }

    

    


    protected override void Patroling()
    {
        moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        

        if (transform.position == targetPoints[currentPoint].position)
        {
            

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
                if (wall!=null && Vector3.Distance(wall.transform.position, transform.position)< maxDashLength)
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
            // ���� ����������� �� ������, ���������������
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
        if (isFinalMiniBoss)
        {
            Destroy(gameObject);
            finalBoss.transform.position = transform.position;
        }
        transform.rotation = Quaternion.Euler(player.position);
        rb.velocity = Vector3.zero;
        base.Death();
    }

}

