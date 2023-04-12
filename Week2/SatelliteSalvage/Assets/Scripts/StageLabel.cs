using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageLabel : MonoBehaviour
{
    private string startText = "";
    private TextMeshProUGUI labelText;
    private int stageNumber;

    private void Start()
    {
        stageNumber = 0;
        labelText = gameObject.GetComponent<TextMeshProUGUI>();
        labelText.SetText(startText);
    }

    void Update()
    {
        if ((int)GameManager.GetGameState() > 1)
        {
            gameObject.SetActive(true);
            if (GameManager.GetStageNumber() != stageNumber)
            {
                stageNumber = GameManager.GetStageNumber();
                labelText.SetText($"Stage {stageNumber}");
            }
        }
    }
}
