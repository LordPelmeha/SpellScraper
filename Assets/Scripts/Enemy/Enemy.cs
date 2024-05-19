using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;




public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Magic magicType;
    protected LayerMask excludeLayers ;
    public float detectionRange;
    public float stoppingRange;

    public double health;

    RaycastHit2D hit;

    public Transform player;
    public Animator animator;
    public Vector3 moveDelta;
    protected float _speedRotate = 5f;
    public int rotationOffset = -90;
    public float rotZ;
    protected Vector3 difference;
    protected Quaternion rotation;

    protected float distanceToPlayer;
    
    protected string animType;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        switch (magicType)
        {
            case Magic.Fire:
                animType = "MoveEnemy";
                break;
            case Magic.Earth:
                animType = "EarthMove";
                break;
            case Magic.Air:
                animType = "WindEnemy";
                break;
            case Magic.Water:
                animType = "WaterMove";
                break;
        }
            

    }

    protected virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer() && (distanceToPlayer > stoppingRange))
        {

            ChasePlayer();
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            
        }
        else if((distanceToPlayer <= detectionRange) && (distanceToPlayer == stoppingRange) && CanSeePlayer())
        {
            rb.velocity = Vector3.zero;
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
        }
        else
        {
            moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
        }

    }



    protected bool CanSeePlayer()
    {

        hit = Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            return true;

        return false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Если столкнулись со стеной, останавливаемся
            rb.velocity = Vector3.zero;
        }
        else if(collision.collider.CompareTag("Projectile"))
        {

            Bullet info = collision.gameObject.GetComponent<Bullet>();
            Magic buletElement = info.element;

            if(!info.enemyBullet)
                TakeDamage(buletElement,info);
        }
        

    }

    public void TakeDamage(Magic buletMagicType,Bullet b)
    {
        int difference = Math.Abs(magicType - buletMagicType);
        switch(difference)
        {
            case 0:
                health -= 0.34;
            break;
            case 2:
                health -= 1;
            break;
            default: break;
        }

        if (health <= 0)
        {
            if (b.emotion == EvilAndKind.Evil)
            {
                PlayerPrefs.SetInt("CountEnd", PlayerPrefs.GetInt("CountEnd") - 1);
            }
            if (b.emotion == EvilAndKind.Kind)
            {
                PlayerPrefs.SetInt("CountEnd", PlayerPrefs.GetInt("CountEnd") + 1);
            }
            Debug.Log(PlayerPrefs.GetInt("CountEnd"));
            Death();
        }
            
    }

    public void Death()
    {
        //сюда анимацию смерти врага
        
        Destroy(gameObject);
    }
    
    protected virtual void ChasePlayer()
    {
        moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        animator.SetFloat(animType, Mathf.Abs(moveDelta.x));
        animator.SetFloat(animType, Mathf.Abs(moveDelta.y));
        animator.SetFloat(animType, Mathf.Abs(moveDelta.magnitude));


        difference = player.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotate);
    }

   

}


