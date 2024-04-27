using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoElevator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene("Elevator");
    }
}
