using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateSatelliteCounter : MonoBehaviour
{
    private string startText = "0 / 1";
    private TextMeshProUGUI labelText;
    // Start is called before the first frame update
    void Start()
    {
        labelText = gameObject.GetComponent<TextMeshProUGUI>();
        labelText.SetText(startText);

    }

    // Update is called once per frame
    void Update()
    {
        if ($"{GameManager.GetSatelliteCount().Item1} / {GameManager.GetSatelliteCount().Item2}" != labelText.text)
        { 
            labelText.SetText($"{GameManager.GetSatelliteCount().Item1} / {GameManager.GetSatelliteCount().Item2}");
        }
    }
}
