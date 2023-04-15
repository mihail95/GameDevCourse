using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip loop1;
    [SerializeField] private AudioClip loop2;
    [SerializeField] private AudioClip loop3;
    [SerializeField] private AudioClip loop4;
    [SerializeField] private AudioClip loop5;
    [SerializeField] private AudioClip loop6;

    private Dictionary<int, AudioClip> audioClips;
    private AudioSource audioSource;
    private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioClips = new Dictionary<int, AudioClip>()
        {
            { 0, loop1 } , { 1, loop2 } , { 2, loop3 } , { 3, loop4 } , { 4, loop5 }, { 5, loop6}
        };

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = loop1;
        audioSource.Play();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        audioClip = audioSource.clip;
        if ((GameManager.GetStageNumber() > audioClips.FirstOrDefault(clip => clip.Value == audioClip).Key) && audioClip != loop6)
        {
            audioSource.loop = false;
            if( (audioClip.samples - audioSource.timeSamples) <= 1024)
            {
                audioSource.clip = audioClips[GameManager.GetStageNumber()];
                audioSource.loop = true;
                audioSource.Play();
            }
        }

    }
}
