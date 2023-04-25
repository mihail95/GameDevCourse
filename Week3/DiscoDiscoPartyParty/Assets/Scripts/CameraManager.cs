using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private static SpriteRenderer backgroundSR;
    [SerializeField] private AudioSource airhornSound;
    [SerializeField] private List<GameObject> airhornGOs;
    private bool airhornsActive;

    private void Start()
    {
        airhornsActive = false;
        player = GameObject.FindGameObjectWithTag("Player");
        backgroundSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(CheatManager.airhornCheat && !airhornsActive)
        {
            airhornsActive = true;
            StartCoroutine(AirHornCheat());

        }
        else if(!CheatManager.airhornCheat && airhornsActive)
        {
            StopCoroutine(AirHornCheat());
            airhornsActive = false;
            airhornSound.Stop();
            foreach (var airhorn in airhornGOs)
            {
                if (airhorn.activeSelf == true) { airhorn.SetActive(false); }
            }
        }
    }
    private void LateUpdate()
    {
        try
        {
            float step = 3.5f * Time.deltaTime;
            Vector2 pos = Vector2.MoveTowards(transform.position, player.transform.position, step);
            transform.position = new Vector3(pos.x, pos.y, -10f);
        }
        catch { Debug.Log("No Player found!"); }
    }

    public static void IncreaseBrightness()
    {
        Color backgroundColor = backgroundSR.color;
        Color.RGBToHSV(backgroundColor, out float currentH, out float currentS, out float currentV);
        backgroundSR.color = Color.HSVToRGB(currentH, currentS, currentV + 0.2f);
    }

    public static void DecreaseBrightness()
    {
        Color backgroundColor = backgroundSR.color;
        Color.RGBToHSV(backgroundColor, out float currentH, out float currentS, out float currentV);
        backgroundSR.color = Color.HSVToRGB(currentH, currentS, currentV - 0.2f);
    }

    public static void SetBrightnessToMin()
    {
        Color backgroundColor = backgroundSR.color;
        Color.RGBToHSV(backgroundColor, out float currentH, out float currentS, out float currentV);
        backgroundSR.color = Color.HSVToRGB(currentH, currentS, 0f);
    }

    public static void SetBrightnessToMax()
    {
        Color backgroundColor = backgroundSR.color;
        Color.RGBToHSV(backgroundColor, out float currentH, out float currentS, out float currentV);
        backgroundSR.color = Color.HSVToRGB(currentH, currentS, 1f);
    }

    private IEnumerator AirHornCheat()
    {
        float delay = Random.Range(6f, 36f);
        while (true)
        {
            airhornSound.Play();
            foreach (var airhorn in airhornGOs)
            {
                if (airhorn.activeSelf == false) { airhorn.SetActive(true); }
            }
            yield return new WaitForSeconds(3f);
            foreach (var airhorn in airhornGOs) { airhorn.SetActive(false); }
            yield return new WaitForSeconds(delay);
        }
       
    }
}
