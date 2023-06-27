using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobScript : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Sphere"))
        {
            MusicManager.PlayButtonSound();
            col.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            objectToActivate.SetActive(true);
        }
    }

}
