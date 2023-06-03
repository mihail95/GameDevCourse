using UnityEngine;

public class SeekTarget : MonoBehaviour
{
    public float rotateSpeed;
    public float speed;
    private GameObject myTarget;
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetTarget(GameObject target)
    {
        myTarget = target;
    }

    private void FixedUpdate()
    {
        if (myTarget != null)
        {
            Vector2 direction = (Vector2)myTarget.transform.position - myRigidbody.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, -transform.right).z;
            myRigidbody.angularVelocity = rotateAmount * rotateSpeed;
            myRigidbody.velocity = transform.right * speed;
        }
    }
}
