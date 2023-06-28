using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    RaycastHit hit;
    public TextMeshProUGUI interactHUDText;

    [Header("Transforms")]
    public GameObject pickUps;
    public GameObject gun;
    public GameObject hudUI;
    public GameObject pauseMenu;
    PauseMenuScript pauseMenuScript;

    [Header("Keybind")]
    public KeyCode interact = KeyCode.F;

    public void Awake()
    {
        pauseMenuScript = pauseMenu.GetComponent<PauseMenuScript>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4))
        {
            if(hit.transform.gameObject.tag == "Interactable")
            {
                interactHUDText.enabled = true;
                if(Input.GetKey(interact))
                {
                    hit.transform.gameObject.SetActive(false);
                }

            }
            else
            {
                interactHUDText.enabled = false;
            }

        }
        else
        {
            interactHUDText.enabled = false;
        }

        if (!pickUps.activeInHierarchy && !pauseMenuScript.GameIsPaused)
        {
            gun.SetActive(true);
            hudUI.SetActive(true);
        }
    }
}