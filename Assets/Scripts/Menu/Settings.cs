using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;
    public AudioSource audioSrc;
    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }
    public void SetMusicVolume(float volume)
    {
        audioSrc.volume = volume;
    }
    public void SetFullscreen(bool isfullscreen)

    {
        Screen.fullScreen = isfullscreen;

    }

    public void SetResolution(int resolutionIndex)

    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void ExitSettings()
    {
        //SceneManager.LoadScene(“Level”);

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("MusicVolumePreference", audioSrc.volume);
        PlayerPrefs.Save();
    }
    public void LoadSettings(int currentResolutionIndex)
    {


        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else

            resolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        if (PlayerPrefs.HasKey("MusicVolumePreference"))
        {
            audioSrc.volume = PlayerPrefs.GetFloat("MusicVolumePreference");
            Console.WriteLine("fafsa");
        }
            
    }
}