using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    public static MusicManager instance;
    public AudioSource musicSource;
    public AudioSource enemyHitSource;

    private static bool musicFading;
    private static float originalVolume;
    private static float currentVolume;

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
    private void Update()
    {
        instance.audioMixer.GetFloat("MusicVol", out currentVolume);
        if (musicFading && currentVolume >= -40f)
        {
            float targetValue = currentVolume - 25f*Time.deltaTime;
            instance.audioMixer.SetFloat("MusicVol", targetValue);
        }
        if (SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            if (musicFading)
            {
                musicFading = false;
                instance.audioMixer.SetFloat("MusicVol", originalVolume);
            }
        }
    }
    public static void FadeMusic()
    {
        instance.audioMixer.GetFloat("MusicVol", out originalVolume);
        musicFading = true;
    }

    public static void PlayEnemyHitSound()
    {
        instance.enemyHitSource.PlayOneShot(instance.enemyHitSource.clip);
    }
}
