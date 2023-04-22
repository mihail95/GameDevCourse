using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool specialDance;
    private float speedMod;
    private Dancer thisDancer;

    private void Start()
    {
        specialDance = false;
        speedMod = 4f;
        thisDancer = GetComponent<Dancer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        // Maybe this would be better than listing all combinations like last time ...
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !thisDancer.isDancing)
        {
            transform.position += speedMod * Time.deltaTime * Vector3.up;
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !thisDancer.isDancing)
        {
            transform.position += speedMod * Time.deltaTime * Vector3.down;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !thisDancer.isDancing)
        {
            transform.position += speedMod * Time.deltaTime * Vector3.left;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !thisDancer.isDancing)
        {
            transform.position += speedMod * Time.deltaTime * Vector3.right;
        }

        // Dances
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && !thisDancer.isDancing)
        {
            StartCoroutine(thisDancer.SideMoves());
        }
        if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && !thisDancer.isDancing)
        {
            StartCoroutine(thisDancer.BarrelRoll());
        }
        if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && !thisDancer.isDancing)
        {
            StartCoroutine(thisDancer.ScaleUpAndDown());
        }
        if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) && !thisDancer.isDancing)
        {
            StartCoroutine(thisDancer.SpecialDance());
        }
    }
}
