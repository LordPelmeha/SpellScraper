﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public double health;

    public float dashStartTime;
    private float dashCooldown;
    public float dashSpeed;
    public float dashDuration;
    private bool deathPos=true;
    private Quaternion rotPos;
    private float shootCooldown;

    private Rigidbody2D rb;
    [Range(0, 10f)] public float speed;


    public Animator animator;

    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        dashCooldown = 0f;
        shootCooldown= 0f;
    }

    void Update()
    {
        moveDelta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

        animator.SetFloat("Move", Mathf.Abs(moveDelta.x));
        animator.SetFloat("Move", Mathf.Abs(moveDelta.y));
        animator.SetFloat("Move", Mathf.Abs(moveDelta.magnitude));

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))&& shootCooldown<=0)
        {
            StartCoroutine(AnimationCorutine());
            shootCooldown = 0.8f;
        }
        shootCooldown-=Time.deltaTime;
        
        if (health<=0)
        {
            if (deathPos)
            {
                rotPos = transform.rotation;
            }
            deathPos = false;
            //deathPos=transform.position; 
            Death();
        }
            
        dashCooldown-=Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            Dash();
    }
    private void Dash()
    {

        if (dashCooldown<=0)
        {

            dashCooldown = dashStartTime;
            rb.velocity = moveDelta.normalized*dashSpeed;
            StartCoroutine(DashCoroutine());
        }
    }
    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashDuration);
        StopCoroutine(DashCoroutine());
        rb.velocity = new Vector3(0, 0, 0);
    }
    IEnumerator AnimationCorutine()
    {
        animator.SetFloat("Magic", 1);
        yield return new WaitForSeconds(0.7f);
        animator.SetFloat("Magic", 0);
    }

    private void FixedUpdate()
    {
        //Передача направление
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Перемещение игрока
        moveDelta = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0); //фиксит баг с поворотом. Можно будет попробовать переделать
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        transform.Translate(Time.deltaTime*speed*moveDelta);

        //Поворот игрока за мышкой
        Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Если столкнулись со стеной, останавливаемся
            
            GetComponent<Player>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
        
    }

    public void Death()
    {
        //сюда анимацию смерти игрока
        animator.SetFloat("Death", 2);
        // rb.simulated=false;
        //rb.constraints= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        // GetComponent<Player>().GetComponent<CapsuleCollider2D>().enabled = false;
        //Destroy(gameObject);
        //SceneManager.LoadScene("Level1");
        //Destroy(gameObject);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // moveDelta = Vector3.zero;
        //transform.position = deathPos; //transform.rotation = Quaternion.identity;
        speed = 0f;
        //rb.freezeRotation = true;
        transform.rotation = rotPos;
        StartCoroutine(restoreAfterDeath());
    }
    private IEnumerator restoreAfterDeath()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
