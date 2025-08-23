using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource bgMusicSource;
    public AudioSource[] sfxSource;

    private float bgMusicVolume = 1.0f;
    private float sfxVolume = 1.0f;
    private float bgMusicTime = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        bgMusicVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignAudioSource();
        if(bgMusicSource != null)
        {
            bgMusicSource.time = bgMusicTime;
            if (!bgMusicSource.isPlaying)
            {
                bgMusicSource.Play();
            }
        }
        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        if(bgMusicSource != null)
        {
            bgMusicSource.volume = bgMusicVolume;
        }
        foreach(var sfx in sfxSource)
        {
            sfx.volume = sfxVolume;
        }
    }

    private void AssignAudioSource()
    {
       bgMusicSource = GameObject.Find("bg")?.GetComponent<AudioSource>();

        if(bgMusicSource != null)
        {
            bgMusicSource.loop = true;
        }

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        sfxSource = System.Array.FindAll(allAudioSources, source => source != bgMusicSource);
    }

    public void SetBGMusicVolume(float volume)
    {
        bgMusicVolume = volume;
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        ApplyVolumeSettings();
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        ApplyVolumeSettings();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if(bgMusicSource != null && bgMusicSource.isPlaying)
        {
            bgMusicTime = bgMusicSource.time;
        }
    }

    public void newScene()
    {
        SceneManager.LoadScene(1);
    }
}
