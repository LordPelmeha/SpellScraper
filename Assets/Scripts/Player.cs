using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    public Image Masks;
    public Sprite fireMask;
    public Sprite waterMask;
    public Sprite airMask;
    public Sprite earthMask;

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
        transform.Translate(moveDelta * Time.deltaTime);

        //Поворот игрока за мышкой
        Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
        //
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Masks.sprite = fireMask;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Masks.sprite = waterMask;
       else if (Input.GetKeyDown(KeyCode.Alpha3))
            Masks.sprite = airMask;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            Masks.sprite = earthMask;

    }
}
