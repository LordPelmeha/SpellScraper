using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHand : ChangeMask
{

    public GameObject evilFireBullet;
    public GameObject evilAirBullet;
    public GameObject evilWaterBullet;
    public GameObject evilEarthBullet;

    public GameObject kindFireBullet;
    public GameObject kindAirBullet;
    public GameObject kindWaterBullet;
    public GameObject kindEarthBullet;

    public Transform shotPoint;

    private float evilTimeBtwShots;
    public float evilStartTimeBtwShots;

    private float kindTimeBtwShots;
    public float kindStartTimeBtwShots;

    void Update()
    {
        //кулдаун стрельбы
        if (evilTimeBtwShots <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                //сюда анимацию стрельбы
                switch (scrollMask)
                {
                    case 0: Instantiate(evilFireBullet, shotPoint.position, transform.rotation); break;
                    case 1: Instantiate(evilAirBullet, shotPoint.position, transform.rotation); break;
                    case 2: Instantiate(evilWaterBullet, shotPoint.position, transform.rotation); break;
                    case 3: Instantiate(evilEarthBullet, shotPoint.position, transform.rotation); break;
                }
                evilTimeBtwShots = evilStartTimeBtwShots;
                Debug.Log(scrollMask);
            }
        }
        else
            evilTimeBtwShots -= Time.deltaTime;

        if (kindTimeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //сюда анимацию стрельбы
                switch (scrollMask)
                {
                    case 0: Instantiate(kindFireBullet, shotPoint.position, transform.rotation); break;
                    case 1: Instantiate(kindAirBullet, shotPoint.position, transform.rotation); break;
                    case 2: Instantiate(kindWaterBullet, shotPoint.position, transform.rotation); break;
                    case 3: Instantiate(kindEarthBullet, shotPoint.position, transform.rotation); break;
                }
                kindTimeBtwShots = kindStartTimeBtwShots;
                Debug.Log(scrollMask);
            }
        }
        else
            kindTimeBtwShots -= Time.deltaTime;
    }
}
