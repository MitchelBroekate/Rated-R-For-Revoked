using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class SettingsMenuScript : MonoBehaviour
{
    [Header("VolumeSliders")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider dialogueSlider;
    public Slider sensSlider;

    [Header("Resolutions")]
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private List<Resolution> filteredResolution;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    public void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        dialogueSlider.value = PlayerPrefs.GetFloat("DialogueVolume", 1f);

        resolutions = Screen.resolutions;
        filteredResolution = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolution.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolution.Count; i++)
        {
            string resolutionOption = filteredResolution[i].width + "X" + filteredResolution[i].height + " " + filteredResolution[i].refreshRate + "Hz";
            options.Add(resolutionOption);

            if (filteredResolution[i].width == Screen.width && filteredResolution[i].height == Screen.height)
            {
                currentResolutionIndex= i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusic(float sliderValue)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFX(float sliderValue)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void SetDialogue(float sliderValue)
    {
        audioMixer.SetFloat("DialogueVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("DialogueVolume", sliderValue);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
