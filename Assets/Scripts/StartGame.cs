using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Измените на клавишу, которую вы хотите использовать
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
