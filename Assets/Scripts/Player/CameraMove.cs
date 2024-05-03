using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform lookAt;
    void LateUpdate()
    {
        if (lookAt != null)
        {
            //Камера следует за игроком
            float deltaX = lookAt.position.x - transform.position.x;
            float deltaY = lookAt.position.y - transform.position.y;
            transform.position += new Vector3(deltaX, deltaY, 0);
        }
        
    }
}
