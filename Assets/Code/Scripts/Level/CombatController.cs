using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int playerMaxHealth = 100;
    [SerializeField] int playerCurrentHealth = 100;
    [SerializeField] int playerAttackPower = 6;
    [SerializeField] int playerDefense = 2;

    [Header("Enemy Stats")]
    [SerializeField] int enemyMaxHealth = 100;
    [SerializeField] int enemyCurrentHealth = 100;
    [SerializeField] int enemyAttackPower = 13;

    [Header("Events")]
    [SerializeField] GameEvent playerDeathEvent;
    [SerializeField] GameEvent playerWinFightEvent;
    [SerializeField] GameEvent playerWinLevelEvent;

    [Header("Level")]
    [SerializeField] LevelSO levelSO;

    // current health, max heath, attack income
    public static Action<int, int, int> onChangePlayerHealth;
    public static Action<int, int, int> onChangeEnemyHealth;

    // states
    [SerializeField] private int currentEnemy = 0;
    [SerializeField] private int totalEnemies = 0;

    public void StartGame()
    {
        currentEnemy = 0;
        totalEnemies = levelSO.Enemies.Count - 1;
        playerCurrentHealth = playerMaxHealth;

        UpdateHealthUI();
    }

    private void SetupEnemy()
    {
        enemyMaxHealth = levelSO.Enemies[currentEnemy].Health;
        enemyCurrentHealth = enemyMaxHealth;
        enemyAttackPower = levelSO.Enemies[currentEnemy].Attack;
    }

    public void UpdateHealthUI()
    {
        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, 0);
        onChangePlayerHealth?.Invoke(playerCurrentHealth, playerMaxHealth, 0);
    }

    public void PlayerAttacks()
    {
        enemyCurrentHealth -= playerAttackPower;

        CheckWinConditions();

        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, playerAttackPower);
    }

    public void PlayerCriticalAttack()
    {
        int criticalDamage = (10 * playerAttackPower / 100) + playerAttackPower;
        enemyCurrentHealth -= criticalDamage;

        CheckWinConditions();

        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, playerAttackPower);
    }

    private void CheckWinConditions()
    {
        if (enemyCurrentHealth <= 0)
        {
            if (currentEnemy < totalEnemies)
            {
                playerWinFightEvent.Raise();
            }
            else if (currentEnemy == totalEnemies)
            {
                playerWinLevelEvent.Raise();
            }
            currentEnemy += 1;
        }
    }

    public void EnemyAttacks()
    {
        int attackIncome = Mathf.Clamp(enemyAttackPower - playerDefense, 0, enemyAttackPower);
        playerCurrentHealth -= attackIncome;

        if(playerCurrentHealth < 0)
        {
            playerDeathEvent.Raise();
        }

        onChangePlayerHealth?.Invoke(playerCurrentHealth, playerMaxHealth, attackIncome);
    }

    public void EncounteredNewEnemy()
    {
        SetupEnemy();
        UpdateHealthUI();
    }
}
