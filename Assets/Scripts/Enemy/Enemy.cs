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

    public float detectionRange;

    public double health;



    public Transform player;
    public Animator animator;
    public Vector3 moveDelta;
    public float _speedRotate = 5f;
    public int rotationOffset = -90;
    public float rotZ;

    protected float distanceToPlayer;
    protected Queue<Vector3> playerPath = new Queue<Vector3>();
    protected Vector3 difference;
    protected Quaternion rotation;
    protected string animType;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (magicType == Magic.Fire)
            animType = "MoveEnemy";
        else if (magicType == Magic.Earth)
            animType = "EarthMove";
    }

    public virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((distanceToPlayer <= detectionRange) && CanSeePlayer())
        {

            MoveEnemy(player.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            playerPath.Clear();
        }
        //else /*if (distanceToPlayer <= detectionRange)*/
        //{
        //    ChasePlayer();
        //}
        else
        {
            playerPath.Clear();
            moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);

            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
            animator.SetFloat(animType, 0);
        }

    }



    protected bool CanSeePlayer()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
            if (hit.collider.gameObject.CompareTag("Player"))
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
    }
    public void EnemyTakeDamage(double damage)
    {
        health -= damage;
        if (health <= 0)
            Death();
    }

    public void Death()
    {
        //сюда анимацию смерти врага
        Destroy(gameObject);
    }
    protected void ChasePlayer()
    {
        playerPath.Enqueue(player.position);
        MoveEnemy(playerPath.Peek());
        Debug.Log($"{playerPath.Peek()} {transform.position} ");
        if (transform.position == playerPath.Peek())
            playerPath.Dequeue();

    }
    protected void MoveEnemy(Vector3 pos)
    {
        moveDelta = new Vector3(transform.position.y, transform.position.x, 0f);
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        animator.SetFloat(animType, Mathf.Abs(moveDelta.x));
        animator.SetFloat(animType, Mathf.Abs(moveDelta.y));
        animator.SetFloat(animType, Mathf.Abs(moveDelta.magnitude));


        difference = pos - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotate);
    }
}


