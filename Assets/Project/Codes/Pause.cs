using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    public void PauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            optionsPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
        else
        {
            isPaused = true;

            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

    }

    public void Play()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        optionsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void BackMenu()
    {
        optionsPanel.SetActive(false);
    }
}
