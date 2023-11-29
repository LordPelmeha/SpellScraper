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


    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    public virtual void Update()
    {

        if (Vector2.Distance(transform.position, player.position) < detectionRange  && CanSeePlayer())
        {

            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

    }

    

    protected bool CanSeePlayer()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider !=null)
            if (hit.collider.gameObject.CompareTag("Player"))
                return true;
        
        return false;
    }



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
