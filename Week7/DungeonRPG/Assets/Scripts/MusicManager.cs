using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip explorationMusic;
    [SerializeField] private AudioClip battleMusic;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip playerHit;
    [SerializeField] private AudioClip playerRun;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip enemyAlert;
    [SerializeField] private AudioClip enemyPowerUp;

    public static MusicManager GetInstance() 
    { 
        if (instance == null) { Debug.LogWarning("No MusicManager Instance available!"); }
        return instance; 
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "pHit":
                soundSource.PlayOneShot(playerHit);
                break;
            case "pRun":
                soundSource.PlayOneShot(playerRun);
                break;
            case "eHit":
                soundSource.PlayOneShot(enemyHit);
                break;
            case "eAlert":
                soundSource.PlayOneShot(enemyAlert);
                break;
            case "ePowUp":
                soundSource.PlayOneShot(enemyPowerUp);
                break;
            default:
                break;
        }
    }

    public void PlayMusic(string musicName)
    {
        musicSource.clip = (musicName == "battle") ? battleMusic : explorationMusic;
        musicSource.Play();
    }
}
