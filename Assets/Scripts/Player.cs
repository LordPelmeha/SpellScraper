using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public double health;


    [Range(0, 10f)] public float speed;

    public Animator animator;


    void Update()
    {
        moveDelta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

        animator.SetFloat("Move", Mathf.Abs(moveDelta.x));
        animator.SetFloat("Move", Mathf.Abs(moveDelta.y));
        animator.SetFloat("Move", Mathf.Abs(moveDelta.magnitude));
    }

    private void FixedUpdate()
    {
        //Передача направление
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Перемещение игрока
        moveDelta = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0); //фиксит баг с поворотом. Можно будет попробовать переделать

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
