using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMeteors : MonoBehaviour
{
    [SerializeField] private int meteorsAmount = 10;
    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;

    private void Start()
    {
        InvokeRepeating(nameof(Fire), 1f, 4f);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / meteorsAmount;
        float angle = startAngle;

        for (int i = 0; i < meteorsAmount + 1; i++)
        {
            float metDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float metDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 metMoveVector = new Vector3(metDirX, metDirY, 0f);
            Vector2 metDir = (metMoveVector - transform.position).normalized;

            GameObject meteor = MeteorPool.meteorPoolInstance.GetBullet();
            meteor.transform.SetPositionAndRotation(transform.position, transform.rotation);
            meteor.SetActive(true);
            meteor.GetComponent<MeteorScript>().SetMoveDirection(metDir);

            angle += angleStep;
        }
    }
}
