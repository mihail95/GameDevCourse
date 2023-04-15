using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private float sliderVal;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = FindFirstObjectByType<AudioSource>();
        sliderVal = audioSource.volume;

        gameObject.GetComponent<Slider>().value = sliderVal;
    }

    // Update is called once per frame
    private void Update()
    {
        sliderVal = gameObject.GetComponent<Slider>().value;
        if (audioSource.volume != sliderVal) { ChangeVolume(sliderVal); }
    }

    private void ChangeVolume(float volume)
    {
       audioSource.volume = volume*Mathf.Log(3);
    }
}
