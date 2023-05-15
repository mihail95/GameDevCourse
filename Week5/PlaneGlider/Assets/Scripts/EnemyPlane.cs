using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    [SerializeField] float horizontalSpeed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] Vector2 direction;

    private void Awake()
    {
        rigidBody.velocity = direction * horizontalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Point"))
        {
            rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
            float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0);
        }
    }
}
