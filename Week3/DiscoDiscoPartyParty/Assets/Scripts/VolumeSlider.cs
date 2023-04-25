using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource airhornAudioSource;
    private float sliderVal;
    private Slider slider;
    
    // Start is called before the first frame update
    private void Start()
    {
        sliderVal = audioSource.volume;
        slider = gameObject.GetComponent<Slider>();
        gameObject.GetComponent<Slider>().value = sliderVal;
    }

    // Update is called once per frame
    private void Update()
    {
        sliderVal = slider.value;
        if (audioSource.volume != sliderVal) { ChangeVolume(sliderVal); }
    }

    private void ChangeVolume(float volume)
    {
       audioSource.volume = volume*Mathf.Log(3);
       airhornAudioSource.volume = volume / 5;
    }
}
