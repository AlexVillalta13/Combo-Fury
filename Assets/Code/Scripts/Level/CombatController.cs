using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Player Stats SO")]
    [SerializeField] PlayerStatsSO permanentPlayerStatsSO;
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;
    [SerializeField] UpgradeInLevelSO upgradesSelected;

    [Header("Level")]
    [SerializeField] LevelSO levelSO;

    // Enemy Stats
    float enemyMaxHealth = 100;
    float enemyCurrentHealth = 100;
    float enemyAttackPower = 13;

    [Title("Events", TitleAlignment = TitleAlignments.Centered)]
    [Title("Player", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] GameEvent onPlayerChangeInCombatStat;
    [SerializeField] GameEvent playerGetsHitAnimation;
    [Title("Level Flow", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] GameEvent playerDeathEvent;
    [SerializeField] GameEvent playerWinFightEvent;
    [SerializeField] GameEvent showUpgradeToChoose;
    [SerializeField] GameEvent playerWinLevelEvent;
    [Title("VFX Activation", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] GameEvent ActivateShieldVFX;
    [SerializeField] GameEvent DeactivateShieldVFX;
    [SerializeField] GameEvent ActivateFireVFX;
    [SerializeField] GameEvent DeactivaFireVFX;
    [SerializeField] GameEvent ActivateRevengeVFX;
    [SerializeField] GameEvent DeactivaRevengeVFX;

    // Actions
    // current health, max heath, attack income
    public static Action<float, float, float> onChangePlayerHealth;
    public static Action<float, float, float> onChangeEnemyHealth;
    public static Action<float> onChangeEnemyAttack;
    public static Action<int> onChangeComboNumber;

    // states
    private int currentEnemy = 0;
    private int totalEnemies = 0;
    int currentComboNumber = 0;

    [Title("Upgrades Stats",TitleAlignment = TitleAlignments.Centered)]
    [Title("Fire", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float fireDamagePercentage = 10f;
    [SerializeField] float timeToDamageFire = 1f;
    float timerFireDamage = 0f;
    [SerializeField] float timeToTurnOffFire = 10f;
    [SerializeField] float comboToFire = 10f;
    float timerToTurnOffFire = 0f;
    int fireLevel = 0;
    bool enemyInFire = false;
    bool shieldActivated = false;
    [Title("Hyper Attack", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float hyperAttackMultiplier = 3f;
    [SerializeField] float comboToHyperAttack = 20f;
    [Title("Hyper Attack", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    bool revengeCharged = false;
    [SerializeField] float furyDamageMultiplier = 2f;




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

        revengeCharged = false;
        DeactivaRevengeVFX.Raise();

        currentEnemy = 0;
        totalEnemies = levelSO.Enemies.Count - 1;

        currentComboNumber = 0;
        onChangeComboNumber(currentComboNumber);

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



    public void SetEnemyInFire()
    {
        enemyInFire = true;
        timerToTurnOffFire = 0f;
        ActivateFireVFX.Raise();
    }

    private void FireDamage()
    {
        timerFireDamage = 0f;
        float damage = inCombatPlayerStatsSO.Attack * fireDamagePercentage / 100 + fireLevel;
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

    public void CalculatePlayerNormalAttack()
    {
        float attackPower = inCombatPlayerStatsSO.Attack;

        PlayerAttacks(attackPower);
    }

    public void CalculatePlayerCriticalAttack()
    {
        float criticalDamage = (10 * inCombatPlayerStatsSO.Attack / 100) + inCombatPlayerStatsSO.Attack;

        PlayerAttacks(criticalDamage);
    }

    private void PlayerAttacks(float attackPower)
    {
        currentComboNumber++;
        onChangeComboNumber(currentComboNumber);

        if (fireLevel > 0)
        {
            if (currentComboNumber % comboToFire == 0)
            {
                SetEnemyInFire();
            }
        }

        if(revengeCharged)
        {
            attackPower *= furyDamageMultiplier;
            revengeCharged = false;
            DeactivaRevengeVFX.Raise();
        }

        if (upgradesSelected.HasUpgrade("HyperAttack") && currentComboNumber % comboToHyperAttack == 0)
        {
            attackPower *= hyperAttackMultiplier;
        }

        enemyCurrentHealth -= attackPower;

        CheckWinConditions();

        UpdateEnemyHealthUI(attackPower);
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

        if (upgradesSelected.HasUpgrade("Revenge"))
        {
            revengeCharged = true;
            ActivateRevengeVFX.Raise();
        }

        currentComboNumber = 0;
        onChangeComboNumber(currentComboNumber);

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

    public void FireUpgradeSelected()
    {
        fireLevel++;
    }
}
