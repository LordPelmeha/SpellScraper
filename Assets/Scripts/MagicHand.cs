using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHand : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;

    public float timeBtwShots;
    public float startTimeBtwShots;


    void Update()
    {
        //кулдаун стрельбы
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
            else
                timeBtwShots -= Time.deltaTime;
        }

    }

    public void DestroyBullet()
    {

    }
}
