using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource clickSource;
    [SerializeField] AudioSource deadSource;

    [SerializeField] AudioClip introClip;
    [SerializeField] AudioClip mainClip;
    [SerializeField] AudioClip deadClip;
    [SerializeField] AudioClip clickClip;

    [SerializeField] TMP_Text volText;

    private void Start()
    {
        musicSource.clip = introClip;
        clickSource.clip = clickClip;
        deadSource.clip = deadClip;

        musicSource.Play();
    }
    
    public void PlayClick() 
    {
        clickSource.PlayOneShot(clickClip);
    }

    public void SetGameMusic(bool isMenu)
    {
        musicSource.clip = isMenu ? introClip : mainClip;
        musicSource.Play();
    }

    public void PlayDeathMusic()
    {
        musicSource.Stop();
        deadSource.Play();
    }

    public void SetVolume(float volume)
    {
        volText.text = $"Game Volume: {volume:F0} dB"; 
        audioMixer.SetFloat("MainVol", volume);
    }
} 
