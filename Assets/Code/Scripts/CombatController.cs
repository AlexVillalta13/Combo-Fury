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

    public void PlayerAttacks()
    {
        enemyCurrentHealth -= playerAttackPower;
        Debug.Log("Player Normal attack");
    }

    public void PlayerCriticalAttack()
    {
        enemyCurrentHealth -= (10 * playerAttackPower / 100) + playerAttackPower;
        Debug.Log("Player Crit attack: " + (10 * playerAttackPower / 100) + playerAttackPower);
    }

    public void EnemyAttacks()
    {
        int attackIncome = Mathf.Clamp(enemyAttackPower - playerDefense, 0, enemyAttackPower);
        playerCurrentHealth -= attackIncome;
        Debug.Log("Enemy attacks");
    }
}
