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
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, z - 90f);
        Debug.Log($"{transform.rotation.z} before");

        Debug.Log($"{transform.rotation.z} after");
        //������� ��������
        if (evilTimeBtwShots <= 0)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                //���� �������� ��������
                switch (scrollMask)
                {
                    case 0: InstantiateWithRotation(evilFireBullet, z); break;
                    case 1: InstantiateWithRotation(evilAirBullet, z); break;
                    case 2: InstantiateWithRotation(evilWaterBullet, z); break;
                    case 3: InstantiateWithRotation(evilEarthBullet, z); break;
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
                //���� �������� ��������
                switch (scrollMask)
                {
                    case 0: Instantiate(kindFireBullet, shotPoint.position, bulletRotation); break;
                    case 1: Instantiate(kindAirBullet, shotPoint.position, bulletRotation); break;
                    case 2: Instantiate(kindWaterBullet, shotPoint.position, bulletRotation); break;
                    case 3: Instantiate(kindEarthBullet, shotPoint.position, bulletRotation); break;
                }
                kindTimeBtwShots = kindStartTimeBtwShots;
            }
        }
        else
            kindTimeBtwShots -= Time.deltaTime;
    }
    void InstantiateWithRotation(GameObject bulletPrefab, float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle - 90f);
        Instantiate(bulletPrefab, shotPoint.position, bulletRotation);
    }
}
