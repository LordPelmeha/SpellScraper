using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoElevator : MonoBehaviour
{
    public FadeScreen fadeScreen;
    private string prev;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            prev = SceneManager.GetActiveScene().name;
            StartCoroutine(FadeAndLoadScene());
        }

    }
    private IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(fadeScreen.FadeIn());

        if (prev.Contains("Boss") || prev.Contains("Level5"))
            SceneManager.LoadScene("Elevator");
        else if (prev.Contains("Level"))
            SceneManager.LoadScene($"Boss{int.Parse(prev[^1].ToString())}");
    }
}
