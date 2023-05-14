using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlaneController playerGO;
    private void Update()
    {
        if (playerGO != null) { transform.position = new Vector3(playerGO.GetPlayerXCoord() + 7f, 0f, -10f); }
        else transform.position = new Vector3(0f , 0f, -10f);
    }

}
