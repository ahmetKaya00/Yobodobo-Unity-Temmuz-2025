using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgMusicSlider, sfxSlider;
    void Start()
    {
        bgMusicSlider.value = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgMusicSlider.onValueChanged.AddListener(SetBGMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetsfxVolume);
    }

    private void SetBGMusicVolume(float value)
    {
        AudioManager.instance.SetBGMusicVolume(value);
    }
    private void SetsfxVolume(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
    }
}
