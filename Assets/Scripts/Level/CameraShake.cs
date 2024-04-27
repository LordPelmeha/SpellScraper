using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float shakeRadius = 1f; // Радиус, в котором объект будет трястись относительно игрока
    public float shakeSpeed = 5f; // Скорость тряски
    public float shakesDuration=10f; // время тряски

    private Vector3 originalPosition; // Исходная позиция объекта

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Если тряски еще не завершены
        if (shakesDuration>0)
        {
            // Генерируем случайную позицию в заданном радиусе относительно игрока
            Vector2 randomDirection = Random.insideUnitCircle.normalized * shakeRadius;
            Vector3 targetPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0f);

            // Плавно перемещаем объект к новой позиции
            transform.position = Vector3.Lerp(transform.position, targetPosition, shakeSpeed * Time.deltaTime);
            shakesDuration-=Time.deltaTime;
            // Если объект достиг целевой позиции
            if (Vector3.Distance(transform.position, targetPosition) < shakeRadius)
            {
                // Если завершены все тряски, возвращаем объект в исходное положение
                if (shakesDuration<0)
                {
                    transform.position = Vector3.Lerp(transform.position, player.position, shakeSpeed * Time.deltaTime);
                }
            }
        }
        else
            transform.position = player.position;
    }

    // Возвращает объект к исходной позиции
}
