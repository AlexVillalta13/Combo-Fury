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
    [SerializeField] GameEvent playerWinEvent;

    [Header("Level")]
    [SerializeField] LevelSO levelSO;

    // current health, max heath, attack income
    public static Action<int, int, int> onChangePlayerHealth;
    public static Action<int, int, int> onChangeEnemyHealth;

    // states
    private int currentEnemy = 0;

    public void StartGame()
    {
        currentEnemy = 0;
        playerCurrentHealth = playerMaxHealth;

        SetupEnemy();
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
        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, playerAttackPower);
    }

    public void PlayerCriticalAttack()
    {
        int criticalDamage = (10 * playerAttackPower / 100) + playerAttackPower;
        enemyCurrentHealth -= criticalDamage;
        
        if(enemyCurrentHealth < 0)
        {
            playerWinEvent.Raise();
        }

        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, playerAttackPower);
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
}
