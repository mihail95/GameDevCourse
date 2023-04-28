using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    [ SerializeField ] private float delay;
    private SpriteRenderer spriteRenderer;
    private bool deathMode;
    private bool thisLightIsRed;
    public static bool redLight;

    
    private void Start()
    {
        deathMode = false;
        thisLightIsRed = false;
        redLight = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GenerateRandomColor();
        delay = Random.Range(0f, 3.5f);
        StartCoroutine(ChangeColor());
    }
    private void Update()
    {
        if (CheatManager.squidgameCheat && !deathMode)
        {
            StopCoroutine(ChangeColor());
            StartCoroutine(SwitchGreenOrRed());
            deathMode = true;
        }
        else if (!CheatManager.squidgameCheat && deathMode)
        {
            StopCoroutine(SwitchGreenOrRed());
            StartCoroutine(ChangeColor());
            deathMode = false;
        }
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(delay);

        while (true) 
        {
            if (!Player.specialDance && !CheatManager.squidgameCheat)
            {
                GenerateRandomColor();
                yield return new WaitForSeconds(0.5f + delay);
            }
            else { yield return null; }
        }
    }

    private void GenerateRandomColor()
    {
        spriteRenderer.color = Random.ColorHSV();
    }

    public IEnumerator SwitchGreenOrRed()
    {
        while (true)
        {
            if (!thisLightIsRed)
            {
                spriteRenderer.color = Color.red;
                thisLightIsRed = true;
                redLight = true;
            }
            else
            {
                spriteRenderer.color = Color.green;
                thisLightIsRed = false;
                redLight = false;
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
