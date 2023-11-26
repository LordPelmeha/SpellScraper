using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2f;
    public float detectionRange = 10f;
    public float shootCooldown;
    public double health;
    public Magic magicType;
    public GameObject enemyBulletPrefab;
    public Transform shotPoint;
    private RaycastHit2D hit;

    private Transform player;
    private float lastShootTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, player.position) < detectionRange && CanSeePlayer())
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Wall"));

            if (hit.collider == null)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }


            if (Time.time > lastShootTime + shootCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    bool CanSeePlayer()
    {
        Vector2 direction = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, LayerMask.GetMask("Wall"));

        // Если луч не сталкивается с препятствием и достигает игрока, то игрок видим
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            return true;
        }
        else
        {
            // Визуализация луча (для отладки)
            Debug.DrawRay(transform.position, direction.normalized * detectionRange, Color.green);
            return false;
        }
    }
    private void Shoot()
    {
        Vector3 shootDirection = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        Instantiate(enemyBulletPrefab, shotPoint.position, bulletRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Если столкнулись со стеной, останавливаемся
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(double damage)
    {
        if (health > 0)
            health -= damage;
        else
            Death();
    }

    public void Death()
    {
        //сюда анимацию смерти врага
        Destroy(gameObject);
    }
}
