using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private float platformSpeed;
    [SerializeField] private bool isHorizontal;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = isHorizontal ? new Vector3(platformSpeed, 0f, 0f) : new Vector3(0f, 0f, platformSpeed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == startPoint ||  collider.gameObject == endPoint)
        {
            rb.velocity = -rb.velocity;
        }
    }
}
