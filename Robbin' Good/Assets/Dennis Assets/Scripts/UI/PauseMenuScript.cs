using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject tutorialPop;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject hudUI;
    public GameObject jack;
    bool settingsMenu = false;
    bool tutorial = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu == false && tutorial == true)
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
        TutorialFinished();
    }

    public void Resume()
    {
        if (jack.activeInHierarchy)
        {
            hudUI.SetActive(true);
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        settingsMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        hudUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void TutorialFinished()
    {
        if (tutorialPop.activeInHierarchy)
        {
            tutorial = false;
        }
        else
        {
            tutorial = true;
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Dennis");
        Time.timeScale = 1f;
    }
}
