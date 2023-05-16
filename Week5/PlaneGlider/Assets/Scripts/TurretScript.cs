using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] float turretRange;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform nozzlePoint;
    [SerializeField] PlaneController playerScript;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float shotDelay;
    [SerializeField] float missileSpeed;

    private Vector2 turretDirection;
    private bool isShooting;

    private void Awake()
    {
        isShooting = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

    private void FixedUpdate()
    {
        Vector3 currentDirection = transform.right;
        Vector2 playerPos = playerTransform.position;
        Vector2 playerDirection = playerPos - (Vector2)transform.position;
        turretDirection = playerDirection - playerScript.GetPlayerVelocity() * -1f; // Aim at player pos in 1 second if no change in velocity (or something close)
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, playerDirection, turretRange); // See if player is in LoS
        
        transform.right = (turretDirection.x < 0 && turretDirection.y > 1) ? turretDirection : currentDirection;

        if (raycastHit)
        {
            if (raycastHit.collider.gameObject.CompareTag("Player") && !isShooting && (turretDirection.x < 0 && turretDirection.y > 1))
            {
                StartCoroutine(nameof(ShootWithDelay));
                isShooting = true;
            }

            else if (raycastHit.collider.gameObject.name.Contains("rock") && isShooting)
            {
                StopCoroutine(nameof(ShootWithDelay));
                isShooting = false;
            }

            else if (raycastHit.collider.gameObject.CompareTag("Player") && !(turretDirection.x < 0 && turretDirection.y > 1) && isShooting)
            {
                StopCoroutine(nameof(ShootWithDelay));
                isShooting = false;
            }
        }
    }

    private IEnumerator ShootWithDelay()
    {
        while (true)
        {
            ShootMissile();
            yield return new WaitForSecondsRealtime(shotDelay);
        }
    }

    private void ShootMissile()
    {
        GameObject missileGO = Instantiate(missilePrefab, nozzlePoint.position, Quaternion.identity);
        missileGO.GetComponent<Rigidbody2D>().AddForce(turretDirection * missileSpeed);
    }
}
