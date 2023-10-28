﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;


    public GameObject bullet;
    public Transform shotPoint;

    public float timeBtwShots;
    public float startTimeBtwShots;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //�������� �����������
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //����������� ������
        moveDelta = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0); //������ ��� � ���������. ����� ����� ����������� ����������
        //��������
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector3(0, moveDelta.y, 0), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Wall"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector3(moveDelta.x, 0, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Wall"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        //transform.Translate(moveDelta * Time.deltaTime);

        //������� ������ �� ������
        Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z - 90);
        Debug.Log(z);
        transform.rotation = Quaternion.Euler(0, 0, z);


        if (timeBtwShots <= 0) 
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
            else
                timeBtwShots -= Time.deltaTime;
        }
    }

    public void Death()
    {
        Destroy(bullet);
    }
}
