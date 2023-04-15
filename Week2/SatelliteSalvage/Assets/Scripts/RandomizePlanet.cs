using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomizePlanet : MonoBehaviour
{
    private static SpriteRenderer atmosphereSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        atmosphereSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public static void RandomizeAtmosphereColor()
    {
        atmosphereSpriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0.3f, 0.6f));
    }
}
