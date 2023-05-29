using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    bool settingsMenu = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        settingsMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SettingsMenu()
    {
        if (settingsMenu == false)
        {
            settingsMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            settingsMenu = true;
        }
        else
        {
            settingsMenuUI.SetActive(false);
            settingsMenu = false;
            
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Dennis");
        Time.timeScale = 1f;
    }
}
