using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    private AudioSource audioSource;
    private string BGM_VOLUME = "BGM_Volume";
    

    private void Awake() {
        if (Instance != null) {
            throw new Exception("SoundManager存在多个Instance!");
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME, 1f);
    }


    public void SetBGMVolume(float vol) {
        audioSource.volume = vol;
        PlayerPrefs.SetFloat(BGM_VOLUME, vol);
    }

}
