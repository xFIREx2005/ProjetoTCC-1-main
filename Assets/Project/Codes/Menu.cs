using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject optionsPanel;

    public void Play()
    {
        SceneManager.LoadScene("CombatArea");
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
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
