using UnityEngine;
using UnityEngine.Playables;

public class Satellite : MonoBehaviour
{
    public enum SatelliteState { OFF, ONE, TWO, ON }
    public SatelliteState satelliteState;

    [SerializeField] private Color colOn, colOff;
    private bool lightsSet;

    private SpriteRenderer antennaLow;
    private SpriteRenderer antennamedium;
    private SpriteRenderer antennaHigh;
    
    private void Start()
    {
        lightsSet = false;
        antennaLow = gameObject.transform.GetChild(5).GetChild(0).GetComponent<SpriteRenderer>();
        antennamedium = gameObject.transform.GetChild(5).GetChild(1).GetComponent<SpriteRenderer>();
        antennaHigh = gameObject.transform.GetChild(5).GetChild(2).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!lightsSet) { SetLights((int)satelliteState); }

    }

    private void SetLights(int lightsNum)
    {
        switch (lightsNum)
        {
            case 0:
                antennaLow.color = colOff;
                antennamedium.color = colOff;
                antennaHigh.color = colOff;
                lightsSet = true;
                break;
            case 1:
                antennaLow.color = colOn;
                antennamedium.color = colOff;
                antennaHigh.color = colOff;
                lightsSet = true;
                break;
            case 2:
                antennaLow.color = colOn;
                antennamedium.color = colOn;
                antennaHigh.color = colOff;
                lightsSet = true;
                break;
            case 3:
                antennaLow.color = colOn;
                antennamedium.color = colOn;
                antennaHigh.color = colOn;
                lightsSet = true;
                GameManager.IncrementSatelliteCount();
                break;
        }
    }

    public int GetSatelliteHealth()
    {
        return (int)satelliteState;
    }

    public void HealSatellite()
    {
        satelliteState++;
        lightsSet = false;
    }
}
