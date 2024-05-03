using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public float shakeRadius = 1f; // ������, � ������� ������ ����� �������� ������������ ������
    public float shakeSpeed = 5f; // �������� ������
    public float shakesDuration=10f; // ����� ������

    private Vector3 originalPosition; // �������� ������� �������

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // ���� ������ ��� �� ���������
        if (shakesDuration>0)
        {
            // ���������� ��������� ������� � �������� ������� ������������ ������
            Vector2 randomDirection = Random.insideUnitCircle.normalized * shakeRadius;
            Vector3 targetPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0f);

            // ������ ���������� ������ � ����� �������
            transform.position = Vector3.Lerp(transform.position, targetPosition, shakeSpeed * Time.deltaTime);
            shakesDuration-=Time.deltaTime;
            // ���� ������ ������ ������� �������
            if (Vector3.Distance(transform.position, targetPosition) < shakeRadius)
            {
                // ���� ��������� ��� ������, ���������� ������ � �������� ���������
                if (shakesDuration<0)
                {
                    transform.position = Vector3.Lerp(transform.position, player.position, shakeSpeed * Time.deltaTime);
                }
            }
        }
        else
            transform.position = player.position;
    }

    // ���������� ������ � �������� �������
}
