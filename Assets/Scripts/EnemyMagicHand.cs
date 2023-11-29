using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.UIElements;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyMagicHand : Enemy
{
    [SerializeField] Enemy en;

    [SerializeField] private Transform shotPoint;
    public GameObject Bullet;

    public float offset;
    private float z;
    private float TimeBtwShots;
    public float StartTimeBtwShots;

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    

    public override void Update()
    {
        Vector3 d = player.transform.position - transform.position;
        z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f,0f,z + offset);

        if(Vector2.Distance(en.transform.position, player.position) < en.detectionRange  && CanSeePlayer())
        {

            if (TimeBtwShots <= 0)
            {
                UnityEngine.Debug.Log("ßÏÈÄÀÐÀÑ");
                InstantiateWithRotation(Bullet);
                TimeBtwShots = StartTimeBtwShots;
            }
            else
                TimeBtwShots -= Time.deltaTime;

        }
    }

    void InstantiateWithRotation(GameObject bulletPrefab)
    {
        //Quaternion bulletRotation = Quaternion.Euler(0, 0, angle - 90f);
        Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
    }
}
