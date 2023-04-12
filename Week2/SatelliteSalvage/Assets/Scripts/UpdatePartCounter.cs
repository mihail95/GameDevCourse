using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePartCounter : MonoBehaviour
{
    private string startText = "0";
    private TextMeshProUGUI labelText;
    // Start is called before the first frame update
    private void Start()
    {
        labelText = gameObject.GetComponent<TextMeshProUGUI>();
        labelText.SetText(startText);

    }

    // Update is called once per frame
    private void Update()
    {
        if ($"{Player.GetPartsCount()}" != labelText.text) { labelText.SetText($"{Player.GetPartsCount()}"); }
    }
}
