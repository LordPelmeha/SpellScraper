using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoElevator : MonoBehaviour
{
    public FadeScreen fadeScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            StartCoroutine(FadeAndLoadScene());
        }
            
    }
    private IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(fadeScreen.FadeIn());
        SceneManager.LoadScene("Elevator");
    }
}
