using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTutorial : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if ((int)GameManager.GetGameState() > 1 && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
