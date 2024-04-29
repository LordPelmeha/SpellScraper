using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBoss : PatrolingEnemy
{
    //Collider2D bulletCollider;
    //Collider2D enemyCollider;
    public Magic[] elements;
    bool DashAble = false;
    Vector3 dashDirection;
    RaycastHit2D hit;
    
    public Rigidbody2D  miniBossRb;
    readonly float _speedRotatee = 30f;
    public float dashSpeed;
    public float dashLength ;

    protected override void Start()
    {
        Invoke(nameof(UnlockDash), 1f);
        //bulletCollider = GetComponent<MiniBossMagicHand>().GetComponent<Bullet>().GetComponent<CapsuleCollider2D>();
        //enemyCollider = GetComponent<CapsuleCollider2D>();
        miniBossRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentPoint = Random.Range(0, targetPoints.Length);

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
            
            //Debug.Log("completing");
            //hit = Physics2D.Linecast(transform.position, dashDirection * 50f, 1 << LayerMask.NameToLayer("Action"));
            DashAble = false;
            Invoke(nameof(UnlockDash), 1f);
            miniBossRb.velocity = new Vector3(0, 0, 0);
            
            dashDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Debug.Log("drawing ray");
            hit = Physics2D.CircleCast(transform.position, dashLength, dashDirection,  LayerMask.NameToLayer("Action"));
            
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Action"))
            {
                Debug.Log("recounting");
                dashDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                hit = Physics2D.CircleCast(transform.position, dashLength, dashDirection, LayerMask.NameToLayer("Action"));
                return;
            }

            Debug.Log("pushing");
            Vector3 move = dashDirection * dashSpeed;
            
            miniBossRb.velocity = move;

            StartCoroutine(DashCoroutine());

            //Debug.Log($" {hit.collider.gameObject.layer == LayerMask.NameToLayer("Action")}, перерасчёт, {dashDirection * 50f}=dashDirection*50, {dashDirection * dashSpeed}=dashDirection * dashSpeed");
        }
    }


    protected virtual IEnumerator DashCoroutine()
    {
        Debug.Log("Awit");
      //  Physics2D.IgnoreCollision(bulletCollider, enemyCollider, true);
        yield return new WaitForSeconds(0.2f);
        //Physics2D.IgnoreCollision(bulletCollider, enemyCollider, false);
        //StopCoroutine(DisableColliderCoroutine());
        StopCoroutine(DashCoroutine());

        miniBossRb.velocity = new Vector3(0, 0, 0);
        ChangeElement();
    }

    protected virtual void UnlockDash()
    {
        DashAble = true;
    }

    protected virtual void ChangeElement()
    {
        Debug.Log("Changed");
        int curMagicType =Random.Range(0,elements.Length);
        magicType = elements[curMagicType];
    }

    //protected virtual IEnumerator DisableColliderCoroutine()
    //{
    //    Debug.Log("Disabled");
    //    Physics2D.IgnoreCollision(bulletCollider, enemyCollider, true);
    //    yield return new WaitForSeconds(0.1f);
    //    Physics2D.IgnoreCollision(bulletCollider, enemyCollider, false);
    //    StartCoroutine(DashCoroutine());

    //}




}

//protected override void ChasePlayer()
//{
//    moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
//    if (moveDelta.magnitude > 1f)
//        moveDelta.Normalize();
//    animator.SetFloat(animType, Mathf.Abs(moveDelta.x));
//    animator.SetFloat(animType, Mathf.Abs(moveDelta.y));
//    animator.SetFloat(animType, Mathf.Abs(moveDelta.magnitude));


//    difference = player.position - transform.position;
//    rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

//    rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
//    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotatee);
//}