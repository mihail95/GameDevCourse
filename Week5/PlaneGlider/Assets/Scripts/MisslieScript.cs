using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MisslieScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;

    private void Awake()
    {
        StartCoroutine(SelfDestruct());
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    } 
}
