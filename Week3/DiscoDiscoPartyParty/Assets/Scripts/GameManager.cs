using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { CameraManager.IncreaseBrightness(); }
        else if (Input.GetKeyDown(KeyCode.O)) { CameraManager.DecreaseBrightness(); }
        else if ( Input.GetKeyDown(KeyCode.F)) { SpawnNewLight(); }
    }

    private void SpawnNewLight()
    {
        Vector3 coords = mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height), 0f));
        coords.z = 0f;
        Instantiate(lightPrefab, coords, Quaternion.identity);
    }
}
