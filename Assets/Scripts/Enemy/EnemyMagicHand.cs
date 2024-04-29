using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.UIElements;
using UnityEngine;
using Unity.VisualScripting;


public class EnemyMagicHand : MonoBehaviour
{
    [SerializeField] protected Enemy Owner;
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform shotPoint;
    public GameObject Bullet;

    public float offset;
    protected float z;
    protected float TimeBtwShots;
    public float StartTimeBtwShots;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    protected virtual void Update()
    {
        Vector3 d = player.transform.position - transform.position;
        z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, z + offset);

        if (Vector3.Distance(Owner.transform.position, player.position) < Owner.detectionRange && CanSeePlayer())
        {

            if (TimeBtwShots <= 0)
            {
                InstantiateWithRotation(Bullet);
                TimeBtwShots = StartTimeBtwShots;
            }
            else
                TimeBtwShots -= Time.deltaTime;

        }
    }

    protected void InstantiateWithRotation(GameObject bulletPrefab)
    {
        //Quaternion bulletRotation = Quaternion.Euler(0, 0, angle - 90f);
        Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
    }
    protected bool CanSeePlayer()
    {

        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
            if (hit.collider.gameObject.CompareTag("Player"))
                return true;

        return false;
    }
}
