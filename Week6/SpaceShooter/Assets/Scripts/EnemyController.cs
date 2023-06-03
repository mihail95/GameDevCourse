using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyProjectilePrefab;
    public float shipSpeed;

    private Rigidbody2D myRigidbody;
    private bool hasChangedDirection;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        if (myRigidbody.transform.position.y > 0)
        {
            myRigidbody.AddForce(Vector2.down * shipSpeed);
        }
        else
        {
            myRigidbody.AddForce(Vector2.up * shipSpeed);
        }

        StartCoroutine(StartShooting());
    }

    private void FixedUpdate()
    {
        if ((myRigidbody.transform.position.y >= 4 || myRigidbody.transform.position.y <= -4) && !hasChangedDirection)
        {
            myRigidbody.velocity = -myRigidbody.velocity;
            hasChangedDirection = true;
            StartCoroutine(DirectionChangeTimer());
        }
    }

    private IEnumerator StartShooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
            GameObject projectileGO = Instantiate(enemyProjectilePrefab, new Vector3(myRigidbody.transform.position.x - 0.5f, myRigidbody.transform.position.y, myRigidbody.transform.position.z), Quaternion.Euler(0, 0, -90));
            projectileGO.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 300f);
        }
        
    }

    private IEnumerator DirectionChangeTimer()
    {
        yield return new WaitForSeconds(1f);
        hasChangedDirection = false;

    }
}
