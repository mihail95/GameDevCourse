using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject meteoritePrefab1;
    public GameObject meteoritePrefab2;
    public float bossSpeed;

    private Rigidbody2D myRigidbody;
    private bool hasChangedDirection;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        if (myRigidbody.transform.position.y > 0)
        {
            myRigidbody.AddForce(Vector2.down * bossSpeed);
        }
        else
        {
            myRigidbody.AddForce(Vector2.up * bossSpeed);
        }

        // StartCoroutine(StartShooting());
    }

    private void FixedUpdate()
    {
        if ((myRigidbody.transform.position.y >= 3.8f || myRigidbody.transform.position.y <= -3.8f) && !hasChangedDirection)
        {
            myRigidbody.velocity = -myRigidbody.velocity;
            hasChangedDirection = true;
            StartCoroutine(DirectionChangeTimer());
        }
    }

    private IEnumerator DirectionChangeTimer()
    {
        yield return new WaitForSeconds(1f);
        hasChangedDirection = false;

    }

}
