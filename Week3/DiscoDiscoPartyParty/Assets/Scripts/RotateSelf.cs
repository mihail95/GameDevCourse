using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(360f * Time.deltaTime * Vector3.up);
    }
}
