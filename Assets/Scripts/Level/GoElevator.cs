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
        if (prev.Contains("Level"))
            SceneManager.LoadScene($"Boss{int.Parse(prev[^1].ToString())}");
        if (prev.Contains("Boss"))
            SceneManager.LoadScene("Elevator");
    }
}
