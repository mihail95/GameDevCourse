using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 0.5f);
        transform.localPosition += offset;
    }
}
