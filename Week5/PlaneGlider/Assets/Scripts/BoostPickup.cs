using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPickup : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(100f * Time.deltaTime * Vector2.down);
    }
}
