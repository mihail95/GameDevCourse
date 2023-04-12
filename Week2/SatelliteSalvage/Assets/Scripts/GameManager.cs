using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using static Player;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EntitySpawner entitySpawner;
    [SerializeField] private List<GameObject> walls = new(4);

    public enum GameState { START, TUTORIAL, TUTORIAL_END, STAGE_PREP, STAGE, STAGE_END, GAME_WON}
    private static GameState gameState = GameState.START;
    private static int satellitesNeeded;
    private static int satellitesRepaired;
    private static Player.PlayerState playerState;
    private static Player player;

    private GameObject playerGO;
    private Dictionary<Player.PlayerState, GameObject> playerGOs = new();
    private Collider2D playerCollider;
    private GameObject playerPod;
    private List<GameObject> gameObjectList;



    // Start is called before the first frame update
    void Start()
    {
        gameObjectList = new();
        // Setup Tutorial
        satellitesNeeded = 1;
        playerGO = entitySpawner.SpawnPlayer(Player.PlayerState.ASTRONAUT, new Vector3(-5, -1, 0));
        player = playerGO.GetComponent<Player>();
        playerCollider = playerGO.GetComponent<Collider2D>();
        playerGOs.Add(Player.PlayerState.ASTRONAUT, playerGO);

        var satellite = entitySpawner.SpawnSatellite(new Vector3(1, 1, 0));
        gameObjectList.Add(satellite);
        
        var asteroid = entitySpawner.SpawnAsteroid(new Vector3(-1, 1, 0));
        gameObjectList.Add(satellite);

        var toolBox = entitySpawner.SpawnToolBox(new Vector3(3, -1, 0));
        gameObjectList.Add(toolBox);
        var toolBox2 = entitySpawner.SpawnToolBox(new Vector3(-1, 2, 0));
        gameObjectList.Add(toolBox2);

        playerPod = entitySpawner.SpawnPlayerPod(new Vector3(15, -4, 0));
        gameState = GameState.TUTORIAL;
    }
    private void StartStage()
    {
        // Cleanup previous stage
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        foreach (GameObject player in playerGOs.Values) { if (player) { Destroy(player); }}
        gameObjectList.Clear();
        playerGOs.Clear();
        Destroy(playerPod);

        // Setup Stage
        satellitesNeeded = 3;
        satellitesRepaired = 0;
        playerGO = entitySpawner.SpawnPlayer(Player.PlayerState.ASTRONAUT, new Vector3(-5, -1, 0));
        player = playerGO.GetComponent<Player>();
        playerCollider = playerGO.GetComponent<Collider2D>();
        playerGOs.Add(Player.PlayerState.ASTRONAUT, playerGO);

        /*
        entitySpawner.SpawnSatellite(new Vector3(1, 1, 0));
        entitySpawner.SpawnAsteroid(new Vector3(-1, 1, 0));
        entitySpawner.SpawnToolBox(new Vector3(3, -1, 0));
        entitySpawner.SpawnToolBox(new Vector3(-1, 2, 0));
        playerPod = entitySpawner.SpawnPlayerPod(new Vector3(15, -4, 0));
        */

        gameState = GameState.STAGE;
    }

    public static GameState GetGameState()
    {
        return gameState;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.TUTORIAL || gameState == GameState.STAGE)
        {
            player = playerGO.GetComponent<Player>();
            CheckPlayerMovementInput();
            CheckPlayerActionInput();

            if (satellitesRepaired == satellitesNeeded)
            {
                if(playerPod.GetComponent<Collider2D>().IsTouching(playerCollider))
                {
                    Debug.Log("Stage Done");
                    gameState = gameState == GameState.TUTORIAL ? GameState.TUTORIAL_END : GameState.STAGE_END;
                }
            }
        }
        if (gameState == GameState.TUTORIAL_END || gameState == GameState.STAGE_END)
        {
            gameState = GameState.STAGE_PREP;
            StartStage();
        }
    }

    private void CheckPlayerMovementInput()
    {   
        // North, East, South, West
        Tuple<int, int, int, int> playerIntersects = CheckPlayerIntersect();
        // Commence Spaghetti
        #region
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (playerIntersects.Item1 == 0) { player.MovePlayer(new Vector3(0, 1, 0)); }    
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (playerIntersects.Item4 == 0) { player.MovePlayer(new Vector3(-1, 0, 0)); player.TurnPlayer(Vector2.left); }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (playerIntersects.Item3 == 0) { player.MovePlayer(new Vector3(0, -1, 0)); }
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (playerIntersects.Item2 == 0) { player.MovePlayer(new Vector3(1, 0, 0)); player.TurnPlayer(Vector2.right); }
        }
        // Who is making me do this ?! - 8 Directional input
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (playerIntersects.Item1 == 0 && playerIntersects.Item2 == 0) { player.MovePlayer(new Vector3(0.71f, 0.71f, 0)); player.TurnPlayer(Vector2.right); }
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (playerIntersects.Item1 == 0 && playerIntersects.Item4 == 0) { player.MovePlayer(new Vector3(-0.71f, 0.71f, 0)); player.TurnPlayer(Vector2.left); }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (playerIntersects.Item3 == 0 && playerIntersects.Item2 == 0) { player.MovePlayer(new Vector3(0.71f, -0.71f, 0)); player.TurnPlayer(Vector2.right); }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (playerIntersects.Item3 == 0 && playerIntersects.Item4 == 0) { player.MovePlayer(new Vector3(-0.71f, -0.71f, 0)); player.TurnPlayer(Vector2.left); }
        }
        #endregion

    }
    private Tuple<int, int, int, int> CheckPlayerIntersect()
    {
        playerCollider = playerGOs[playerState].GetComponent<Collider2D>();

        int intersectNorth = 0;
        int intersectEast = 0;
        int intersectSouth = 0;
        int intersectWest = 0;

        foreach (GameObject wall in walls)
        {
            if (wall.GetComponent<Collider2D>().IsTouching(playerCollider))
            {
                switch (wall.name)
                {
                    case "Border 1": // East
                        intersectEast = 1;
                        break;
                    case "Border 2": // West
                        intersectWest = 1;
                        break;
                    case "Border 3": // South
                        intersectSouth = 1;
                        break;
                    case "Border 4": // North
                        intersectNorth = 1;
                        break;
                }
            }
        }

        return new Tuple<int, int, int, int>(intersectNorth, intersectEast, intersectSouth, intersectWest);
    }
    private void CheckPlayerActionInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.SetRandomSpriteColors();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.GetComponent<Collider2D>().IsTouchingLayers()) { player.UseAbility(); }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (player.GetMovementState() == Player.MovementState.NORMAL)
            {
                player.SetMovementState(Player.MovementState.BOOST);
            }
            else if (player.GetMovementState() == Player.MovementState.BOOST)
            {
                player.SetMovementState(Player.MovementState.SUPER_BOOST);
            }
            else if (player.GetMovementState() == Player.MovementState.SUPER_BOOST)
            {
                player.SetMovementState(Player.MovementState.NORMAL);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (playerState == Player.PlayerState.ROBOT) 
            {   
                Vector3 curentPos = playerGO.transform.position;
                playerState = Player.PlayerState.ASTRONAUT;
                playerGO = playerGOs[PlayerState.ASTRONAUT];
                player = playerGO.GetComponent<Player>();
                player.transform.position = curentPos;
                playerCollider = playerGO.GetComponent<Collider2D>();
                playerGO.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (playerState == Player.PlayerState.ASTRONAUT) 
            {
                Vector3 curentPos = playerGO.transform.position;
                // If player, change to drone
                // If no drone instance, spawn drone and then change state
                if (!playerGOs.ContainsKey(Player.PlayerState.ROBOT))
                {
                    playerGO = entitySpawner.SpawnPlayer(Player.PlayerState.ROBOT, player.transform.position);
                    player = playerGO.GetComponent<Player>();
                    playerCollider = playerGO.GetComponent<Collider2D>();
                    playerGOs.Add(Player.PlayerState.ROBOT, playerGO);
                }
                else
                {
                    playerGO = playerGOs[PlayerState.ROBOT];
                    player = playerGO.GetComponent<Player>();
                    playerCollider = playerGO.GetComponent<Collider2D>();
                }
                player.transform.position = curentPos;
                playerState = Player.PlayerState.ROBOT;
                playerGO.SetActive(true);
            }    
        }
    }

    public static Player.PlayerState GetPlayerState() { return playerState; }
    public static Player GetCurrentPlayer() { return player; }

    public static void IncrementSatelliteCount()
    {
        satellitesRepaired++;
    }

    public static Tuple<int, int> GetSatelliteCount() 
    {
        return new Tuple<int, int>(satellitesRepaired, satellitesNeeded);   
    }
}
