using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private AudioClip raceMusicClip;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip coinPickupClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip buttonClip;

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

    public static void PlayCoinSound()
    {
        instance.soundSource.Stop();
        instance.soundSource.PlayOneShot(instance.coinPickupClip);
    }

    public static void PlayJumpSound()
    {
        instance.soundSource.Stop();
        instance.soundSource.PlayOneShot(instance.jumpClip);
    }

    public static void PlayButtonSound()
    {
        instance.soundSource.Stop();
        instance.soundSource.PlayOneShot(instance.buttonClip);
    }
}
