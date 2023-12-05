using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
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

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            StartCoroutine(AnimationCorutine());
        if (health<=0)
            Death();

    }

    IEnumerator AnimationCorutine()
    {
        animator.SetFloat("Magic", 1);
        yield return new WaitForSeconds(0.7f);
        animator.SetFloat("Magic", 0);
    }

    private void FixedUpdate()
    {
        //Передача направление
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Перемещение игрока
        moveDelta = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0); //фиксит баг с поворотом. Можно будет попробовать переделать
        if (moveDelta.magnitude > 1f)
            moveDelta.Normalize();
        transform.Translate(moveDelta * Time.deltaTime*speed);

        //Поворот игрока за мышкой
        Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    public void Death()
    {
        //сюда анимацию смерти игрока
        Destroy(gameObject);
        SceneManager.LoadScene("Level1");
    }
}
