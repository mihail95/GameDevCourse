using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioSource musicSource;
    
    public static MusicManager GetInstance() 
    { 
        if (instance == null) { Debug.LogWarning("No MusicManager Instance available!"); }
        return instance; 
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
