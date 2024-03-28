using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy : Enemy
{


    public Transform[] spots;
    private int currentSpot;
    public Transform currentGoal;



    public override void Update()
    {
        if ((Vector3.Distance(transform.position, player.position) < detectionRange) && CanSeePlayer())
        {
            moveSpeed *= 2;

            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        //else if (Vector3.Distance(transform.position, player.position) <= stoppingDistance && (Vector3.Distance(transform.position, player.position) < detectionRange))
        //    transform.position = this.transform.position;
        else
        {
            if (Vector3.Distance(transform.position, spots[currentSpot].position) < 0.2)
                transform.position = Vector3.MoveTowards(transform.position, spots[currentSpot].position, moveSpeed * Time.deltaTime);
            else
                ChangeSpot();
        }
    }

    private void ChangeSpot() 
    {
        if (currentSpot == spots.Length - 1)
        {
            currentSpot = 0;
            currentGoal = spots[0];
        }
        else
        {
            currentSpot++;
            currentGoal = spots[currentSpot];
        }
    }



}
