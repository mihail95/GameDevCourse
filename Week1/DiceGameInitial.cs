using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

enum GameState { gameStart, stageStart, playerTurn, enemyTurn, gameWon, gameLost }

public class DiceGame : MonoBehaviour
{
    GameState gameState = GameState.gameStart;
    int playerHP;
    int enemyHP;
    int maxPlayerHP;
    int maxEnemyHP;
    bool isNegating = false;
    bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHP = 10;
        maxPlayerHP = 10;
        Debug.Log("Greetings, human! Let the games begin.");
        gameState = GameState.stageStart;
        StartStage();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                PlayerAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                PlayerHeal();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                PlayerDefend();
            }
        }
        else if (gameState == GameState.enemyTurn)
        {
            MakeComputerMove();
        }
        else if (gameState == GameState.gameWon)
        {
            Debug.Log("YOU WON! Congrats!");
            Quit();
        }
        else if (gameState == GameState.gameLost)
        {
            Debug.Log("YOU DIED!");
            Quit();
        }
    }

    void StartStage()
    {
        playerHP = maxPlayerHP;
        enemyHP = maxEnemyHP;
        gameState = GameState.playerTurn;
        Debug.Log("Your turn, press 1 to ATTACK, 2 to HEAL or 3 to DEFEND!");
    }
    void PlayerAttack()
    {
        int damage = Random.Range(1, 3);
        enemyHP -= damage;
        Debug.Log($"You attacked for {damage} damage! Enemy HP: {enemyHP}");
        if (enemyHP <= 0)
        {
            gameState = GameState.gameWon;
        }
        else SwitchTurn();
    }
    void PlayerHeal()
    {
        int heal = playerHP <= maxPlayerHP - 2 ? 2 : (playerHP == maxPlayerHP - 1 ? 1 : 0);
        playerHP += heal;
        Debug.Log($"You restored {heal} HP! Player HP: {playerHP}");
        SwitchTurn();
    }
    void PlayerDefend()
    {
        isNegating = true;
        Debug.Log("You defended!");
        SwitchTurn();
    }
    void SwitchTurn()
    {
        if (gameState == GameState.playerTurn)
        {
            gameState = GameState.enemyTurn;
            Debug.Log("My turn now!");
        } 
        else if (gameState == GameState.enemyTurn)
        {
            gameState = GameState.playerTurn;
            isNegating = false;
            Debug.Log("Your turn again! Press 1 to ATTACK, 2 to HEAL or 3 to DEFEND!");
        }
    }
    void MakeComputerMove()
    {
        if (isCharging)
        {
            DealChargeDamage();
        }
        if (gameState == GameState.enemyTurn)
        {
            float diceRoll = Random.value;
            if (diceRoll <= 0.65f)
            {
                ComputerAttack();
            }
            else
            {
                ComputerCharge();
            }
        }
    }
    void ComputerAttack()
    {
        if (isNegating) 
        {
            Debug.Log($"You negated the attack!");
        }
        else 
        {
            int damage = Random.Range(2, 5);
            playerHP -= damage;
            Debug.Log($"You got smacked for {damage} damage! Player HP: {playerHP}");
        }

        if (playerHP <= 0)
        {
            gameState = GameState.gameLost;
        }
        else SwitchTurn();
    }
    void ComputerCharge()
    {
        Debug.Log($"I am charging a devastating attack!");
        isCharging = true;
        SwitchTurn();
    }
    void DealChargeDamage()
    {
        if (!isNegating) 
        {
            playerHP = 0;
            Debug.Log($"You got hit by a charged attack. Player HP: {playerHP}");
            gameState = GameState.gameLost;
        }
        else 
        {
            Debug.Log($"You negated the charged attack!");
        }
        isCharging = false;
    }
    void Quit()
    {
        /* 
         * "Borrowed" someones code from stackoverflow (1.) - handles the quit action depending on the game context (Unity Editor or Standalone Game)
         * Hashes (#) are used for conditional compilation, giving us access to some scripting symbols (see documentation 2.)
         * 1. https://stackoverflow.com/questions/70437401/cannot-finish-the-game-in-unity-using-application-quit 
         * 2. https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
        */

        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
