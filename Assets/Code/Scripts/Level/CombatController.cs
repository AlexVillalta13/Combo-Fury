using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerMaxHealth = 100;
    public int playerCurrentHealth = 100;
    public int playerAttackPower = 6;
    public int playerDefense = 2;

    [Header("Enemy Stats")]
    public int enemyMaxHealth = 100;
    public int enemyCurrentHealth = 100;
    public int enemyAttackPower = 13;

    [Header("Events")]
    [SerializeField] GameEvent playerDeathEvent;
    [SerializeField] GameEvent playerWinEvent;

    [Header("Level")]
    [SerializeField] LevelSO levelSO;

    public static Action<int, int, int> onChangePlayerHealth;
    public static Action<int, int, int> onChangeEnemyHealth;

    private void Start()
    {
        StartCombat();
    }

    public void StartCombat()
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
