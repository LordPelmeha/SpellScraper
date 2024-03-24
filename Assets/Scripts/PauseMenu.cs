using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private bool pause;
    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pause)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }
    public void Pause()
    {

        pauseMenu.SetActive(true); 
        Time.timeScale = 0f;
        pause = true;
    }
    public void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
