using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    [ SerializeField ] private float delay;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GenerateRandomColor();
        delay = Random.Range(0f, 3.5f);
        StartCoroutine(ChangeColor());
    }
    
    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(delay);

        while (true) 
        {
            if (!Player.specialDance)
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
    
}
