using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke(nameof(Destroy), 4f);
    }

    private void Start()
    {
        moveSpeed = 200f;
        gameObject.GetComponent<Rigidbody2D>().AddForce(moveDirection * moveSpeed);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
        gameObject.GetComponent<Rigidbody2D>().AddForce(moveDirection * moveSpeed);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
