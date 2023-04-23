using UnityEngine;

public class Conductor : MonoBehaviour
{
    private float crotchet; // Seconds per beat
    private float dspSongTime; // Seconds passed since song start
    private float songPosition; // Current position in song (seconds)
    private float songPositionInBeats; // Current position in song (beats)
    public static Conductor instance;  //Conductor instance

    [SerializeField] private float songBpm; // BPM
    [SerializeField] private AudioSource musicSource; // Music Source

    public int pulseCounter; // Calculates the quarter note pulse (range 1-4)

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        crotchet = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
    }

    // Start is called before the first frame update
    private void Start()
    {
        musicSource.Play();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime); // how many seconds since start?
        songPositionInBeats = (songPosition / crotchet) + 1; // how many beats since start?
        pulseCounter = (int)songPositionInBeats % 4 + 1;
    }
}
