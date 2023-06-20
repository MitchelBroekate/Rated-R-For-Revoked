using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    RaycastHit hit;

    [Header("Transforms")]
    public GameObject pickUps;
    public GameObject gun;
    public GameObject hudUI;

    [Header("Keybind")]
    public KeyCode interact = KeyCode.F;
    void Start()
    {
        
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4))
        {
            if(hit.transform.gameObject.tag == "Interactable")
            {
                if(Input.GetKey(interact))
                {
                    hit.transform.gameObject.SetActive(false);
                }
                
            }

        }

        if (pickUps.activeInHierarchy == false)
        {
            gun.SetActive(true);
            hudUI.SetActive(true);
        }
    }
}