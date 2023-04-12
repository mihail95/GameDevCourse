using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static GameManager;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.GetGameState() == GameState.TUTORIAL || GameManager.GetGameState() == GameState.STAGE)
        {
            // Smooth
            try 
            { 
                player = GameObject.FindGameObjectWithTag("Player"); 
                Vector3 posDiff = new(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
                transform.position += 2 * Time.deltaTime * posDiff;
            }
            catch { Debug.Log("No Player found!"); } 
        }
    }
}
