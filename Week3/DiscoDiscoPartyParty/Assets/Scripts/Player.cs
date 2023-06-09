using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool specialDance;
    private float speedMod;
    private Dancer thisDancer;
    private SpriteRenderer mySpriteRenderer;
    private bool isNinja;

    private void Start()
    {
        isNinja = false;
        specialDance = false;
        speedMod = 4f;
        thisDancer = GetComponent<Dancer>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for ninja cheat first
        if (CheatManager.ninjaCheat != isNinja) { ToggleNinjaMode(); }
        // Movement
        // Maybe this would be better than listing all combinations like last time ...
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { transform.position += speedMod * Time.deltaTime * Vector3.up; }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { transform.position += speedMod * Time.deltaTime * Vector3.down; }
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { transform.position += speedMod * Time.deltaTime * Vector3.left; }
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { transform.position += speedMod * Time.deltaTime * Vector3.right; }
        }

        // Dances
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { StartCoroutine(thisDancer.SideMoves()); }
        }
        if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { StartCoroutine(thisDancer.BarrelRoll()); }
        }
        if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { StartCoroutine(thisDancer.ScaleUpAndDown()); }
        }
        if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) && !thisDancer.isDancing)
        {
            if (CheatManager.squidgameCheat && DiscoLight.redLight) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            else { StartCoroutine(thisDancer.SpecialDance()); }   
        }
    }

    private void ToggleNinjaMode()
    {
        speedMod = CheatManager.ninjaCheat ? 2f : 4f;
        mySpriteRenderer.color = new Color(1f, 1f, 1f, CheatManager.ninjaCheat ? 0.5f : 1f);
        isNinja = !isNinja;
    }
}
