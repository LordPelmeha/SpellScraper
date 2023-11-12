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
        Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg - 90;
        Debug.Log($"{transform.rotation.z} before");

        Debug.Log($"{transform.rotation.z} after");
        //кулдаун стрельбы
        if (evilTimeBtwShots <= 0)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                //сюда анимацию стрельбы
                switch (scrollMask)
                {
                    case 0: Instantiate(evilFireBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                    case 1: Instantiate(evilAirBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                    case 2: Instantiate(evilWaterBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                    case 3: Instantiate(evilEarthBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                }
                evilTimeBtwShots = evilStartTimeBtwShots;
                
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
                    case 0: Instantiate(kindFireBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                    case 1: Instantiate(kindAirBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                    case 2: Instantiate(kindWaterBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                    case 3: Instantiate(kindEarthBullet, shotPoint.position, Quaternion.Euler(0, 0, z)); break;
                }
                kindTimeBtwShots = kindStartTimeBtwShots;
            }
        }
        else
            kindTimeBtwShots -= Time.deltaTime;
    }
}
