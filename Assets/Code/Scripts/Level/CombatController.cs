using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] PlayerStatsSO permanentPlayerStatsSO;
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;
    //[SerializeField] int playerMaxHealth = 100;
    //public int PlayerMaxHealth { get { return playerMaxHealth; } set { playerMaxHealth = value; } }
    //[SerializeField] int playerCurrentHealth = 100;
    //public int PlayerCurrentHealth { get { return playerCurrentHealth; } set { playerCurrentHealth = value; } }
    //[SerializeField] int playerAttackPower = 6;
    //public int PlayerAttackPower { get { return playerAttackPower; } set { playerAttackPower = value; } }
    //[SerializeField] int playerDefense = 2;
    //public int PlayerDefense { get { return playerDefense; } set { playerDefense = value; } }

    [Header("Enemy Stats")]
    [SerializeField] int enemyMaxHealth = 100;
    public int EnemyMaxHealth { get { return enemyMaxHealth; } set { enemyMaxHealth = value; } }
    [SerializeField] int enemyCurrentHealth = 100;
    public int EnemyCurrentHealth { get { return enemyCurrentHealth; } set { enemyCurrentHealth = value; } }
    [SerializeField] int enemyAttackPower = 13;
    public int EnemyAttackPower { get { return enemyAttackPower; } set { enemyAttackPower = value; } }

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

    private void OnEnable()
    {
        LevelSelectorUI.loadLevel += SetupLevel;
    }
    private void OnDisable()
    {
        LevelSelectorUI.loadLevel -= SetupLevel;
    }

    public void StartGame()
    {
        inCombatPlayerStatsSO.MaxHealth = permanentPlayerStatsSO.MaxHealth;
        inCombatPlayerStatsSO.Attack = permanentPlayerStatsSO.Attack;
        inCombatPlayerStatsSO.Defense = permanentPlayerStatsSO.Defense;
        inCombatPlayerStatsSO.CriticalAttackChance = permanentPlayerStatsSO.CriticalAttackChance;
        inCombatPlayerStatsSO.CurrentHealth = inCombatPlayerStatsSO.MaxHealth;

        currentEnemy = 0;
        totalEnemies = levelSO.Enemies.Count - 1;

        SetupEnemy();
        UpdateEnemyHealthUI(0);
        UpdatePlayerHealthUI(0);
    }

    private void SetupEnemy()
    {
        enemyMaxHealth = levelSO.Enemies[currentEnemy].Health;
        enemyCurrentHealth = enemyMaxHealth;
        //SetEnemyAttack();
    }

    public void SetEnemyAttack()
    {
        enemyAttackPower = levelSO.Enemies[currentEnemy].Attack;
    }

    public void UpdateEnemyHealthUI(int valueDifference)
    {
        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, valueDifference);
    }
    public void UpdatePlayerHealthUI(int valueDifference)
    {
        onChangePlayerHealth?.Invoke(inCombatPlayerStatsSO.CurrentHealth, inCombatPlayerStatsSO.MaxHealth, valueDifference);
    }

    public void PlayerAttacks()
    {
        enemyCurrentHealth -= inCombatPlayerStatsSO.Attack;

        CheckWinConditions();

        UpdateEnemyHealthUI(inCombatPlayerStatsSO.Attack);
    }

    public void PlayerCriticalAttack()
    {
        int criticalDamage = (10 * inCombatPlayerStatsSO.Attack / 100) + inCombatPlayerStatsSO.Attack;
        enemyCurrentHealth -= criticalDamage;

        CheckWinConditions();

        UpdateEnemyHealthUI(inCombatPlayerStatsSO.Attack);
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
        // Animation event when receive damage

        int attackIncome = Mathf.Clamp(enemyAttackPower - inCombatPlayerStatsSO.Defense, 0, enemyAttackPower);
        enemyAttackPower = levelSO.Enemies[currentEnemy].Attack;
        inCombatPlayerStatsSO.CurrentHealth -= attackIncome;

        if(inCombatPlayerStatsSO.CurrentHealth < 0)
        {
            playerDeathEvent.Raise();
        }

        UpdatePlayerHealthUI(attackIncome);
        if(enemyAttackPower == 0)
        {
            SetEnemyAttack();
        }
    }

    public void EncounteredNewEnemy()
    {
        SetupEnemy();
        UpdateEnemyHealthUI(0);
        UpdatePlayerHealthUI(0);
    }

    private void SetupLevel(LevelSO level)
    {
        levelSO = level;
    }
}
