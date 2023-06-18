using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleGameImproved : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHPText;
    [SerializeField] Image playerHPBar;
    [SerializeField] TextMeshProUGUI enemyHPText;
    [SerializeField] Image enemyHPBar;
    [SerializeField] List<Button> buttonList;
    [SerializeField] TextMeshProUGUI parryButtonText;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI bonusDmgText;
    [SerializeField] TextMeshProUGUI stunnedText;
    [SerializeField] Light2D stageLight;
    [SerializeField] Animator enemyAnimator;
    enum GameState { gameStart, stageStart, playerTurn, enemyTurn, gameWon, gameLost }
    GameState gameState = GameState.gameStart;
    int stunCtr, playerHP, enemyHP, maxPlayerHP, maxEnemyHP;
    bool isNegating, isCharging, isStunned, hasParried;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyHP = 20;
        maxPlayerHP = 15;
        gameState = GameState.stageStart;
        StartStage();
    }
    void StartStage()
    {
        stunCtr = 0;
        isNegating = false;
        isCharging = false;
        isStunned = false;
        hasParried = false;
        playerHP = maxPlayerHP;
        enemyHP = maxEnemyHP;
        gameState = GameState.playerTurn;
    }
    public void PlayerAttack()
    {
        int damage = Random.Range(2, 3);
        if (isStunned) { damage *= 2; }
        enemyHP -= damage;
        statusText.text = $"You deal {damage} damage.";
        enemyHPBar.fillAmount = (float) enemyHP / maxEnemyHP;
        enemyHPText.SetText($"{enemyHP}/{maxEnemyHP}");
        enemyAnimator.SetBool("isHit", true);
        if (enemyHP <= 0) { gameState = GameState.gameWon; QuitBattle(); }
        else StartCoroutine(WaitAndSwitchTurn());
    }
    public void PlayerHeal()
    {
        int heal = Mathf.Min(Random.Range(4, 5), maxPlayerHP - playerHP);
        playerHP += heal;
        playerHPBar.fillAmount = (float) playerHP / maxPlayerHP;
        playerHPText.SetText($"{playerHP}/{maxPlayerHP}");
        statusText.text = $"You heal for {heal} HP.";
        StartCoroutine(WaitAndSwitchTurn());
    }

    public void PlayerDeflect()
    {
        if (!isStunned) 
        {
            statusText.text = $"You prepare to parry.";
            isNegating = true; 
        }
        else 
        {
            statusText.text = $"You strike your enemy, reducing its attack bonus.";
            stunCtr = Mathf.Max(stunCtr-1, 0);
            bonusDmgText.SetText($"+{stunCtr}");
            hasParried = true;
        }

        StartCoroutine(WaitAndSwitchTurn());
    }
    public void PlayerRun()
    {
        QuitBattle();
    }
    void SwitchTurn()
    {
        if (gameState == GameState.playerTurn)
        {
            gameState = GameState.enemyTurn;
            MakeComputerMove();
        }
        else if (gameState == GameState.enemyTurn)
        {
            gameState = GameState.playerTurn;
            isNegating = false;
            parryButtonText.text = isStunned ? "Riposte" : "Parry";
            foreach (Button button in buttonList)
            {
                button.interactable = !button.interactable;
            }
        }
    }
    void MakeComputerMove()
    {
        if (isCharging) { DealChargeDamage(); }
        else
        {
            if (!isStunned)
            {
                float diceRoll = Random.value;
                if (diceRoll < 0.75f) { ComputerAttack(); }
                else { ComputerCharge(); }
            }
            else
            {
                if (!hasParried)
                {
                    stunCtr += 1;
                    bonusDmgText.SetText($"+{stunCtr}");
                    statusText.text = $"The enemy recovers from the stun and gains an attack bonus.";
                }
                else
                {
                    statusText.text = $"The enemy recovers from the stun but doesn't get stronger.";
                }

                isStunned = false;
                stunnedText.enabled = false;
                StartCoroutine(WaitAndSwitchTurn());
            }
        }
    }
    void DealChargeDamage()
    {
        if (!isNegating)
        {
            playerHP = 0;
            playerHPBar.fillAmount = (float) playerHP / maxPlayerHP;
            playerHPText.SetText($"{playerHP}/{maxPlayerHP}");
            statusText.text = $"You got hit by a charged attack.";
            gameState = GameState.gameLost;
            QuitBattle();
        }
        else
        {
            statusText.text = $"You negated the charged attack and stunned the opponent!";
            isNegating = false;
            isStunned = true;
            stunnedText.enabled = true;
            StartCoroutine(WaitAndSwitchTurn());
        }
        isCharging = false;
    }
    void ComputerAttack()
    {
        enemyAnimator.SetBool("isAttacking", true);
        int damage = Random.Range(2, 5) + stunCtr;
        playerHP -= damage;
        playerHPBar.fillAmount = (float) playerHP / maxPlayerHP;
        playerHPText.SetText($"{playerHP}/{maxPlayerHP}");

        if (isNegating)
        {
            statusText.text = $"You tried to parry, but the attack was too fast. The enemy hit you for {damage} damage!";
            isNegating = false;
        }
        else { statusText.text = $"The enemy hit you for {damage} damage!"; }
        


        if (playerHP <= 0) { gameState = GameState.gameLost; QuitBattle(); }
        else StartCoroutine(WaitAndSwitchTurn());
    }
    void ComputerCharge()
    {
        statusText.text = "Your enemy is charging a devastating attack!";
        isCharging = true;
        StartCoroutine(WaitAndSwitchTurn());
    }

    private void QuitBattle()
    {
        foreach (Button button in buttonList)
        {
            button.interactable = false;
        }
        if ( gameState == GameState.gameLost ) { statusText.text = "You Died!"; }
        else if ( gameState == GameState.gameWon ) { statusText.text = "You Won"; }
        StartCoroutine(FadeToBlack());
    }

    public IEnumerator WaitAndSwitchTurn()
    {
        if (gameState == GameState.playerTurn)
        {
            foreach (Button button in buttonList)
            {
                button.interactable = !button.interactable;
            }
        }
        yield return new WaitForSeconds(2f);
        SwitchTurn();
    }

    public IEnumerator FadeToBlack()
    {
        while (stageLight.intensity > 0f) 
        {
            stageLight.intensity -= 1f * Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("ExplorationScene");
    }
}
