using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed ;
    public float detectionRange ;
    public float retreatDistance;
    public float shootCooldown;
    public LayerMask playerLayer;
    public double health;
    public Magic magicType;
    public GameObject enemyBulletPrefab;
    public Transform shotPoint;
    public RaycastHit2D hit;
    public LayerMask obstacleLayer;

    public Transform player;
    public float lastShootTime;


    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    public virtual void Update()
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
        
        
        

    }


    bool CanSeePlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        RaycastHit hit;

        if (Physics.Linecast(player.position, transform.position, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }


    //private void Shoot()
    //{
    //    Vector3 shootDirection = (player.position - transform.position).normalized;
    //    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
    //    Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

    //    Instantiate(enemyBulletPrefab, shotPoint.position, bulletRotation);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Если столкнулись со стеной, останавливаемся
            rb.velocity = Vector2.zero;
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
}
