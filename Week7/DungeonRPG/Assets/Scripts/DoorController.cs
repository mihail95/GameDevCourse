using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject doorHitbox;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject playerGO;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject ==  playerGO)
        {
            playerGO.transform.position = spawnPoint.transform.position;
        }
    }
}
