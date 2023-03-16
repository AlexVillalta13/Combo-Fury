using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] PlayerStatsSO permanentPlayerStatsSO;
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;
    [SerializeField] UpgradeInLevelSO upgradesSelected;

    [Header("Enemy Stats")]
    [SerializeField] float enemyMaxHealth = 100;
    public float EnemyMaxHealth { get { return enemyMaxHealth; } set { enemyMaxHealth = value; } }
    [SerializeField] float enemyCurrentHealth = 100;
    public float EnemyCurrentHealth { get { return enemyCurrentHealth; } set { enemyCurrentHealth = value; } }
    [SerializeField] float enemyAttackPower = 13;
    public float EnemyAttackPower { get { return enemyAttackPower; } set { enemyAttackPower = value; } }

    [Header("Events")]
    [SerializeField] GameEvent onPlayerChangeInCombatStat;
    [SerializeField] GameEvent playerDeathEvent;
    [SerializeField] GameEvent playerWinFightEvent;
    [SerializeField] GameEvent showUpgradeToChoose;
    [SerializeField] GameEvent playerWinLevelEvent;
    [SerializeField] GameEvent ActivateShieldVFX;
    [SerializeField] GameEvent DeactivateShieldVFX;

    [Header("Level")]
    [SerializeField] LevelSO levelSO;

    // Events
    // current health, max heath, attack income
    public static Action<float, float, float> onChangePlayerHealth;
    public static Action<float, float, float> onChangeEnemyHealth;
    public static Action<float> onChangeEnemyAttack;

    // states
    [SerializeField] private int currentEnemy = 0;
    [SerializeField] private int totalEnemies = 0;
    [SerializeField] bool shieldActivated = false;

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

        onPlayerChangeInCombatStat.Raise();

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
        SetEnemyAttack();

        ActivateShield();
    }

    private void ActivateShield()
    {
        if(upgradesSelected.HasUpgrade("Shield") && shieldActivated == false)
        {
            shieldActivated = true;
            ActivateShieldVFX.Raise();
            //Shield animation
        }
    }

    public void SetEnemyAttack()
    {
        enemyAttackPower = levelSO.Enemies[currentEnemy].Attack;
        onChangeEnemyAttack?.Invoke(enemyAttackPower);
    }

    public void UpdateEnemyHealthUI(float valueDifference)
    {
        onChangeEnemyHealth?.Invoke(enemyCurrentHealth, enemyMaxHealth, valueDifference);
    }
    public void UpdatePlayerHealthUI(float valueDifference)
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
        float criticalDamage = (10 * inCombatPlayerStatsSO.Attack / 100) + inCombatPlayerStatsSO.Attack;
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
                StartCoroutine(ShowUpgrades());
            }
            else if (currentEnemy == totalEnemies)
            {
                playerWinLevelEvent.Raise();
            }
            currentEnemy += 1;
        }
    }

    private IEnumerator ShowUpgrades()
    {
        yield return new WaitForSeconds(1f);
        showUpgradeToChoose.Raise();
    }

    public void EnemyAttacks()
    {
        // Animation event when receive damage
        if (shieldActivated == true)
        {
            shieldActivated = false;
            DeactivateShieldVFX.Raise();
            UpdatePlayerHealthUI(0);
            return;
        }


        float attackIncome = Mathf.Clamp(enemyAttackPower - inCombatPlayerStatsSO.Defense, 1, enemyAttackPower);
        inCombatPlayerStatsSO.CurrentHealth -= attackIncome;

        if(inCombatPlayerStatsSO.CurrentHealth < 0)
        {
            playerDeathEvent.Raise();
        }

        UpdatePlayerHealthUI(attackIncome);

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
