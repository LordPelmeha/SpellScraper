using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossMagicHand : EnemyMagicHand
{

    [SerializeField] GameObject[] Bullets;

    protected override void Update()
    {
        Vector3 d = player.transform.position - transform.position;
        z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;

        Bullet= Bullets[Random.Range(0, Bullets.Length)];

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
}
