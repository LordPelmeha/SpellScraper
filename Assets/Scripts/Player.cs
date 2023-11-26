using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public double health;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //Передача направление
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Перемещение игрока
        moveDelta = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0); //фиксит баг с поворотом. Можно будет попробовать переделать

        //Коллизия
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector3(0, moveDelta.y, 0), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Wall"));
        //if (hit.collider == null)
        //    transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector3(moveDelta.x, 0, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Wall"));
        //if (hit.collider == null)
        //    transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        transform.Translate(moveDelta * Time.deltaTime);

        //Поворот игрока за мышкой
        Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    public void Death()
    {
        //сюда анимацию смерти игрока
        Destroy(gameObject);
    }
}
