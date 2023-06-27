using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    // This is a parent object that contains your entire menu panel
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menuButton;
    [SerializeField] TMP_Text musicVolText;
    [SerializeField] TMP_Text effectVolText;

    [SerializeField] AudioMixer audioMixer;

    // This toggles the menu on or off
    public void ShowMenu() => menu.SetActive(!menu.activeSelf);

    public void SetMusicVolume(float volume)
    {
        musicVolText.text = $"Music Volume: {volume:F0} dB";
        audioMixer.SetFloat("MusicVol", volume);
    }

    public void SetEffectVolume(float volume)
    {
        effectVolText.text = $"Sound Effects Volume: {volume:F0} dB";
        audioMixer.SetFloat("EffectsVol", volume);
    }
}
