using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private static SpriteRenderer backgroundSR;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        backgroundSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
        float currentH, currentS, currentV;
        Color.RGBToHSV(backgroundColor, out currentH, out currentS, out currentV);
        backgroundSR.color = Color.HSVToRGB(currentH, currentS, currentV + 0.2f);
    }

    public static void DecreaseBrightness()
    {
        Color backgroundColor = backgroundSR.color;
        Color.RGBToHSV(backgroundColor, out float currentH, out float currentS, out float currentV);
        backgroundSR.color = Color.HSVToRGB(currentH, currentS, currentV - 0.2f);
    }
}
