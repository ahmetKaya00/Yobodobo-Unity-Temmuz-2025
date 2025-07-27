using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    private Transform camera;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        camera = Camera.main.transform;
    }
    void Update()
    {
        float mesafe = Vector3.Distance(transform.position, camera.position);
        float maxMesafe = 10f;
        float normalizedMesafe = Mathf.Clamp01(mesafe / maxMesafe);
        audioSource.volume = 1f - normalizedMesafe;
    }
}
