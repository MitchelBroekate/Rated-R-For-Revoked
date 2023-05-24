using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [Header("Checks")]
    public bool synthCheck = false;
    public bool electroCheck = false;

    [Header("Game Objects")]
    public GameObject musicSynth;
    public GameObject musicElectro;

    // Update is called once per frame
    void Update()
    {
        if (synthCheck == false)
        {
            musicElectro.SetActive(true);
        }
        else
        {
            musicSynth.SetActive(false);
        }
    }
}
