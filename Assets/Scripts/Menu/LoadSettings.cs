using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSrc;
    void Start()
    {
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        if (PlayerPrefs.HasKey("MusicVolumePreference"))
        {
            audioSrc.volume = PlayerPrefs.GetFloat("MusicVolumePreference");
            Console.WriteLine("fafsa");
        }
    }


}
