using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private List<string> cheatList;
    private string cheatQueue;
    public static bool dogeCheat;

    private void Start()
    {
        dogeCheat = false;
        cheatQueue = "";
        cheatList = new List<string>
        {
            "ninja", // -50% move speed + transperant
            "doge", // change all animals to doge
            "squidgame", // disco lights go red light, green light - destroy animal if dancing while red, reload if player does anything while red
            "airhorn" // summon 4 airhorns that spin around, play airhorn.wav
        };
    }
    private void Update()
    {
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            cheatQueue += Input.inputString.ToLower();
        }
        if (cheatQueue.Length > 9)
        {
            cheatQueue = cheatQueue[1..];
        }
        foreach (string cheatString in cheatList)
        {
            if (cheatQueue.Contains(cheatString))
            {
                switch (cheatString)
                {
                    case "doge":
                        if (!dogeCheat) { dogeCheat = true; } else { dogeCheat = false; }
                        break;
                }
                cheatQueue = "";
            }
        }
        
    }
}
