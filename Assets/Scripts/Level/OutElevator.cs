using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutElevator : MonoBehaviour
{
    public GameObject door;
    string previousScene;
    private float activationDelay;
    public FadeScreen fadeScreen;
    void Start()
    {
        activationDelay = GameObject.Find("shaker").GetComponent<CameraShake>().shakesDuration;
        previousScene = PlayerPrefs.GetString("PreviousScene");
        door.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(fadeScreen.Inlight());
        StartCoroutine(ActivateObjectDelayed()); // ��������� ��������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (previousScene != "Level5")
            {
                SceneManager.LoadScene($"Level{int.Parse(previousScene[^1].ToString()) + 1}");
            }
            else
                SceneManager.LoadScene("End");
        }
    }

    IEnumerator ActivateObjectDelayed()
    {
        yield return new WaitForSeconds(activationDelay);

        door.GetComponent<Collider2D>().enabled = true;
        StopCoroutine(ActivateObjectDelayed()); // ������������� ���������� ��������
    }
}
