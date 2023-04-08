using UnityEngine;

public class BattleGameImproved : MonoBehaviour
{
    enum GameState { gameStart, stageStart, playerTurn, enemyTurn, gameWon, gameLost }
    GameState gameState = GameState.gameStart;
    int roundNumber, stunCtr, playerHP, enemyHP, maxPlayerHP, maxEnemyHP;
    bool isNegating, isCharging, isStunned, isHealing;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHP = 30;
        maxPlayerHP = 15;
        Debug.Log("Greetings, human! Let the games begin.");
        gameState = GameState.stageStart;
        StartStage();
    }
    void StartStage()
    {
        stunCtr = 0;
        roundNumber = 1;
        isNegating = false;
        isCharging = false;
        isStunned = false;
        isHealing = false;
        playerHP = maxPlayerHP;
        enemyHP = maxEnemyHP;
        gameState = GameState.playerTurn;
        Debug.Log("Your turn, press 1 to ATTACK, 2 to HEAL, 3 to DEFLECT!");
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.playerTurn) { MakePlayerMove(); }
        else if (gameState == GameState.enemyTurn) { MakeComputerMove(); }
        else if (gameState == GameState.gameWon) 
        {
            Debug.Log("YOU WON! Congrats! But can you do it again?");
            StartStage();
        }
        else if (gameState == GameState.gameLost)
        {
            Debug.Log("YOU DIED. Try Again!");
            StartStage();
        }
    }

    void MakePlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { PlayerAttack(); }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { PlayerHeal(); }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { PlayerDeflect(); }
    }
    void PlayerAttack()
    {
        int damage = Random.Range(2, 4);
        if (isStunned) { damage *= 2; }
        enemyHP -= damage;
        int heal = Mathf.Min(1, maxPlayerHP - playerHP);
        playerHP += heal; ;
        Debug.Log($"You attacked for <color=orange>{damage} damage</color> and gained <color=green>{heal} HP</color> back! <b>Player HP: {playerHP}  Enemy HP: {enemyHP}</b>");

        if (enemyHP <= 0) gameState = GameState.gameWon;
        else SwitchTurn();
    }
    void PlayerHeal()
    {
        isHealing = true;
        Debug.Log($"You charge up a <color=green>heal</color> for next turn. <b>Player HP: {playerHP}</b>");
        SwitchTurn();
    }
    void RestoreHP()
    {
        int heal = Mathf.Min(Random.Range(4, 7) + stunCtr, maxPlayerHP - playerHP);
        playerHP += heal;
        isHealing = false;
        Debug.Log($"Your charged up heal restores <color=green>{heal} HP</color>. <b>Player HP: {playerHP}</b>");
    }
    void PlayerDeflect()
    {
        isNegating = true;
        SwitchTurn();
    }
    void SwitchTurn()
    {
        if (gameState == GameState.playerTurn)
        {
            gameState = GameState.enemyTurn;
        } 
        else if (gameState == GameState.enemyTurn)
        {
            gameState = GameState.playerTurn;
            isNegating = false;
            roundNumber++;
            Debug.Log($"<b>Round {roundNumber}!</b> <color=green><b>Player HP: {playerHP}</b></color>  <color=red><b>Enemy HP: {enemyHP}</b></color>");
            if (isHealing) { RestoreHP(); }
            Debug.Log("Your turn again! Press 1 to ATTACK, 2 to HEAL, 3 to DEFLECT!");
        }
    }
    void MakeComputerMove()
    {
        if (isCharging) { DealChargeDamage(); }
        if (gameState == GameState.enemyTurn) // Skip this block in case of fatal blow
        {
            if (!isStunned)
            {
                float diceRoll = Random.value;
                if (diceRoll < 0.75f) { ComputerAttack(); }
                else { ComputerCharge(); }
            }
            else 
            {
                stunCtr += 1;
                isStunned = false;
                Debug.Log($"The enemy recovers from the stun. <color=red><b>+{stunCtr} damage to enemy attacks</b></color>");
                SwitchTurn();
            }
        }
    }
    void DealChargeDamage()
    {
        if (!isNegating)
        {
            playerHP = 0;
            Debug.Log($"You got hit by a charged attack. <b>Player HP: {playerHP}</b>");
            gameState = GameState.gameLost;
        }
        else
        {
            Debug.Log($"You negated the charged attack and stunned the opponent! Your heal now restores <color=green><b>+{stunCtr+1} HP</b></color>");
            isNegating = false;
            isStunned = true;
            SwitchTurn();
        }
        isCharging = false;
    }
    void ComputerAttack()
    {
        if (isNegating) 
        {
            Debug.Log($"You cant deflect normal attacks, silly!");
            isNegating = false;
        }    
        int damage = Random.Range(2, 6) + stunCtr;
        playerHP -= damage;
        Debug.Log($"The enemy hit you for <color=red>{damage} damage</color>! <b>Player HP: {playerHP}</b>");


        if (playerHP <= 0) { gameState = GameState.gameLost; }
        else SwitchTurn();
    }
    void ComputerCharge()
    {
        Debug.Log("<color=red><b>I am charging a devastating attack!</b></color>"); 
        isCharging = true;
        SwitchTurn();
    }
}
