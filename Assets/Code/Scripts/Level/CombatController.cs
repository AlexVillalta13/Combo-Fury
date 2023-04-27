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
    [SerializeField] GameEvent playerGetsHitAnimation;
    [SerializeField] GameEvent playerDeathEvent;
    [SerializeField] GameEvent playerWinFightEvent;
    [SerializeField] GameEvent showUpgradeToChoose;
    [SerializeField] GameEvent playerWinLevelEvent;
    [SerializeField] GameEvent ActivateShieldVFX;
    [SerializeField] GameEvent DeactivateShieldVFX;
    [SerializeField] GameEvent ActivateFireVFX;
    [SerializeField] GameEvent DeactivaFireVFX;

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

    [SerializeField] float fireDamagePercentage = 10f;
    [SerializeField] float timeToDamageFire = 3f;
    float timerFireDamage = 0f;
    [SerializeField] float timeToTurnOffFire = 10f;
    float timerToTurnOffFire = 0f;
    int fireLevel = 0;
    bool enemyInFire = false;

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

        shieldActivated = false;

        fireLevel = 0;
        enemyInFire = false;

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

    private void Update()
    {
        if (enemyInFire == true)
        {
            timerFireDamage += Time.deltaTime;
            timerToTurnOffFire += Time.deltaTime;
            if (timerFireDamage >= timeToDamageFire)
            {
                FireDamage();
            }

            if (timerToTurnOffFire >= timeToTurnOffFire)
            {
                TurnOffEnemyFire();
            }
        }
    }

    public void FireUpgradeSelected()
    {
        fireLevel++;
    }

    public void SetEnemyInFire()
    {
        enemyInFire = true;
        timerToTurnOffFire = 0f;
        ActivateFireVFX.Raise();
    }

    private void FireDamage()
    {
        timerFireDamage = 0f;
        float damage = inCombatPlayerStatsSO.Attack * 10 / 100 + fireLevel;
        Debug.Log(damage);
        enemyCurrentHealth -= damage;
        UpdateEnemyHealthUI(damage);
    }

    private void TurnOffEnemyFire()
    {
        enemyInFire = false;
        DeactivaFireVFX.Raise();
    }

    private void ActivateShield()
    {
        if(upgradesSelected.HasUpgrade("Shield") && shieldActivated == false)
        {
            shieldActivated = true;
            ActivateShieldVFX.Raise();
        }
    }

    public void PlayerAttacks()
    {
        if (fireLevel > 0)
        {
            SetEnemyInFire();
        }

        enemyCurrentHealth -= inCombatPlayerStatsSO.Attack;

        CheckWinConditions();

        UpdateEnemyHealthUI(inCombatPlayerStatsSO.Attack);
    }

    public void PlayerCriticalAttack()
    {
        if(fireLevel > 0)
        {
            SetEnemyInFire();
        }

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
                TurnOffEnemyFire();
                playerWinFightEvent.Raise();
                StartCoroutine(ShowUpgrades());
            }
            else if (currentEnemy == totalEnemies)
            {
                TurnOffEnemyFire();
                playerWinLevelEvent.Raise();
            }
            currentEnemy += 1;
        }
    }

    public void EnemyAttacks()
    {
        if (shieldActivated == true)
        {
            shieldActivated = false;
            DeactivateShieldVFX.Raise();
            UpdatePlayerHealthUI(0);
            return;
        }

        float attackIncome = Mathf.Clamp(enemyAttackPower - inCombatPlayerStatsSO.Defense, 1, enemyAttackPower);
        inCombatPlayerStatsSO.CurrentHealth -= attackIncome;
        playerGetsHitAnimation.Raise();

        if(inCombatPlayerStatsSO.CurrentHealth < 0)
        {
            playerDeathEvent.Raise();
        }

        UpdatePlayerHealthUI(-attackIncome);

    }

    private IEnumerator ShowUpgrades()
    {
        yield return new WaitForSeconds(2f);
        showUpgradeToChoose.Raise();
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
