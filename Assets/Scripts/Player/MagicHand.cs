using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicHandType { Player, Enemy }

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
    //private float z;

    private float evilTimeBtwShots;
    public float evilStartTimeBtwShots;

    private float kindTimeBtwShots;
    public float kindStartTimeBtwShots;

    

    void Update()
    {
        //Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;



        //Quaternion bulletRotation = Quaternion.Euler(0, 0, z - 90f);

        //кулдаун стрельбы
        if (evilTimeBtwShots <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                
                switch (scrollMask)
                {
                    case 0: InstantiateWithRotation(evilFireBullet); break;
                    case 1: InstantiateWithRotation(evilEarthBullet); break;
                    case 2: InstantiateWithRotation(evilWaterBullet); break;
                    case 3: InstantiateWithRotation(evilAirBullet); break;
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
                
                switch (scrollMask)
                {
                    case 0: InstantiateWithRotation(kindFireBullet); break;
                    case 1: InstantiateWithRotation(kindEarthBullet); break;
                    case 2: InstantiateWithRotation(kindWaterBullet); break;
                    case 3: InstantiateWithRotation(kindAirBullet); break;
                }
                kindTimeBtwShots = kindStartTimeBtwShots;
            }
        }
        else
            kindTimeBtwShots -= Time.deltaTime;
    }
    void InstantiateWithRotation(GameObject bulletPrefab)
    {
        //Quaternion bulletRotation = Quaternion.Euler(0, 0, angle - 90f);
        Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
    }
}
