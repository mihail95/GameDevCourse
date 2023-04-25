using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private List<string> cheatList;
    private string cheatQueue;
    public static bool dogeCheat;
    public static bool ninjaCheat;
    public static bool airhornCheat;
    public static bool squidgameCheat;

    private void Start()
    {
        ninjaCheat = false;
        dogeCheat = false;
        airhornCheat = false;
        squidgameCheat = false;
        cheatQueue = "";
        cheatList = new List<string>
        {
            "ninja", // -50% move speed + transperant
            "doge", // change all animals to doge
            "squidgame", // disco lights go red light, green light - destroy animal if dancing while red, reload if player does anything while red
            "airhorn" // summon 2 airhorns that spin around and play airhorn.wav every X seconds (generated randomly)
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
                    case "ninja":
                        if (!ninjaCheat) { ninjaCheat = true; } else { ninjaCheat = false; }
                        break;

                    case "doge":
                        if (!dogeCheat) { dogeCheat = true; } else { dogeCheat = false; }
                        break;

                    case "airhorn":
                        if (!airhornCheat) { airhornCheat = true; } else { airhornCheat = false; }
                        break;

                    case "squidgame":
                        if (!squidgameCheat) { squidgameCheat = true; } else { squidgameCheat = false; }
                        break;
                }
                cheatQueue = "";
            }
        }
        
    }
}
