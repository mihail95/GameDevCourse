using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private Dancer thisDancer;
    private float delay;
    [SerializeField] private Sprite dogeSprite;
    private SpriteRenderer animalSpriteRenderer;
    private Sprite startingSprite;
    private Sprite currentSprite;

    // Start is called before the first frame update
    private void Start()
    {
        animalSpriteRenderer = GetComponent<SpriteRenderer>();
        startingSprite = animalSpriteRenderer.sprite;
        thisDancer = GetComponent<Dancer>();
        delay = Random.Range(0f, 10f);
        StartCoroutine(NPCDance());
    }

    private void Update()
    {
        currentSprite = GetComponent<SpriteRenderer>().sprite;
        if (CheatManager.dogeCheat && currentSprite == startingSprite)
        {
            animalSpriteRenderer.sprite = dogeSprite;
        }
        else if (!CheatManager.dogeCheat && currentSprite == dogeSprite)
        {
            animalSpriteRenderer.sprite = startingSprite;
        }
    }

    private IEnumerator NPCDance()
    {
        while (true)
        {
            if (!thisDancer.isDancing)
            {
                ExecuteRandomNormalDance();
                delay = Random.Range(0f, 10f);
                yield return new WaitForSeconds(delay);
            }
            else { yield return null; }
        }
    }

    private void ExecuteRandomNormalDance()
    {
        int rng = Random.Range(0, 3);
        switch (rng)
        {
            case 0:
                StartCoroutine(thisDancer.SideMoves());
                break;
            case 1:
                StartCoroutine(thisDancer.BarrelRoll());
                break;
            case 2:
                StartCoroutine(thisDancer.ScaleUpAndDown());
                break;
        }
    }
}
