using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Enemy : MonoBehaviour
{
    //Блок переменных для логики
    public float triggerLength = 10;
    public float shotRange = 10;

    public bool isTriggered;
    public Transform playerTransform;
    public Vector3 startingPosition;
    public float offset;

    //Блок переменных перемещения и коллизии
    public BoxCollider2D boxCollider;
    public Vector3 moveDelta;
    public float speed;

    //Блок переменных отвечающих за стрельбу
    public GameObject bullet;
    public Transform shotPoint;
    public float timeBtwShots;
    private float rotZ;
    private Vector3 difference;
    public float startTimeBtwShots;
    private Player player;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerTransform = FindAnyObjectByType<Player>().transform;

    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < triggerLength)
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            //transform.rotarion = Quaternion.Euler(0f, 0f, rotZ + offset);

            if (Vector3.Distance(player.transform.position, transform.position) < shotRange)
                if (timeBtwShots <= 0)
                {
                    Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                    timeBtwShots -= Time.deltaTime;
            Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
