using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.UIElements;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyMagicHand : MonoBehaviour
{
    [SerializeField] Enemy en;
    [SerializeField] Transform player;
    [SerializeField] private Transform shotPoint;
    public GameObject Bullet;

    public float offset;
    private float z;
    private float TimeBtwShots;
    public float StartTimeBtwShots;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    private void Update()
    {
        Vector3 d = player.transform.position - transform.position;
        z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, z + offset);

        if (Vector3.Distance(en.transform.position, player.position) < en.detectionRange && CanSeePlayer())
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
    protected bool CanSeePlayer()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
            if (hit.collider.gameObject.CompareTag("Player"))
                return true;

        return false;
    }
}
