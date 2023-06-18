using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Vector3 playerPos;

    public static GameManager GetInstance()
    {
        if (instance == null) { Debug.LogWarning("No MenuManager Instance available!"); }
        return instance;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
