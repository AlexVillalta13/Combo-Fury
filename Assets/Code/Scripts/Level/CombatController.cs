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
    [SerializeField] float enemyCurrentHealth = 100;
    float enemyAttackPower = 13;

    [Title("Events", TitleAlignment = TitleAlignments.Centered)]
    [Title("Player", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] GameEvent onPlayerChangeInCombatStat;
    [SerializeField] GameEvent playerGetsHitAnimation;
    [SerializeField] GameEvent playerDodges;
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
    public static Action<int, int> onChangeCurrentEnemy;

    // states
    private int currentEnemy = 0;
    private int totalEnemies = 0;
    int currentComboNumber = 0;

    [Title("Upgrades Stats", TitleAlignment = TitleAlignments.Centered)]
    [Title("Fire", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float fireDamagePercentage = 10f;
    [SerializeField] float timeToDamageFire = 1f;
    float timerFireDamage = 0f;
    [SerializeField] float timeToTurnOffFire = 10f;
    float timerToTurnOffFire = 0f;
    int fireLevel = 0;
    bool enemyInFire = false;

    bool shieldActivated = false;
    const string shieldId = "Shield";

    [Title("Hyper Attack", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float hyperAttackMultiplier = 3f;
    [SerializeField] float comboToHyperAttack = 20f;
    const string hyperAttackId = "HyperAttack";

    [Title("Revenge", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float revengeDamageMultiplier = 2f;
    bool revengeCharged = false;
    const string revengeId = "Revenge";

    [Title("Spines", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float spinesDamageMultiplier = 0.5f;
    const string spinesId = "Spines";

    [Title("Adrenaline", HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] float adrenalineHealthToActivate = 30f;
    [SerializeField] float adrenalineChance = 20f;
    const string adrenalineId = "Adrenaline";

    [Title("DEBUG", TitleAlignment = TitleAlignments.Centered)]
    [GUIColor("red")]
    public bool godMode = false;


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
        inCombatPlayerStatsSO.StartGame(permanentPlayerStatsSO);

        onPlayerChangeInCombatStat.Raise();

        shieldActivated = false;

        fireLevel = 0;
        enemyInFire = false;
        timerFireDamage = 0f;
        timerToTurnOffFire = 0f;

        revengeCharged = false;
        DeactivaRevengeVFX.Raise();

        currentEnemy = 0;
        totalEnemies = levelSO.Enemies.Count - 1;
        onChangeCurrentEnemy(currentEnemy + 1, levelSO.Enemies.Count);

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
        enemyCurrentHealth -= damage;
        UpdateEnemyHealthUI(-damage);
        CheckWinConditions();
    }

    private void TurnOffEnemyFire()
    {
        timerFireDamage = 0f;
        enemyInFire = false;
        DeactivaFireVFX.Raise();
    }

    private void ActivateShield()
    {
        if(upgradesSelected.HasUpgrade(shieldId) && shieldActivated == false)
        {
            shieldActivated = true;
            ActivateShieldVFX.Raise();
        }
    }

    public void CalculatePlayerNormalAttack()
    {
        PlayerAttacks(inCombatPlayerStatsSO.Attack);
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

        if (revengeCharged)
        {
            attackPower *= revengeDamageMultiplier;
            revengeCharged = false;
            DeactivaRevengeVFX.Raise();
        }

        if (upgradesSelected.HasUpgrade(hyperAttackId) && currentComboNumber % comboToHyperAttack == 0)
        {
            attackPower *= hyperAttackMultiplier;
        }

        EnemyReceivesDamage(attackPower);
    }

    private void EnemyReceivesDamage(float attackPower)
    {
        enemyCurrentHealth -= attackPower;

        CheckWinConditions();

        UpdateEnemyHealthUI(-attackPower);
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
            TurnOffEnemyFire();

            currentEnemy += 1;
            onChangeCurrentEnemy(currentEnemy + 1, levelSO.Enemies.Count);
        }
    }

    public void EnemyAttacks()
    {
        if(upgradesSelected.HasUpgrade(adrenalineId))
        {
            if(inCombatPlayerStatsSO.CurrentHealth < inCombatPlayerStatsSO.MaxHealth * adrenalineHealthToActivate / 100f)
            {
                if (UnityEngine.Random.Range(0f, 100f) < adrenalineChance)
                {
                    playerDodges.Raise();
                    return;
                }
            }
        }

        if (shieldActivated == true)
        {
            shieldActivated = false;
            DeactivateShieldVFX.Raise();
            UpdatePlayerHealthUI(0);
            return;
        }

        if (upgradesSelected.HasUpgrade(revengeId))
        {
            revengeCharged = true;
            ActivateRevengeVFX.Raise();
        }

        currentComboNumber = 0;
        onChangeComboNumber(currentComboNumber);

        float attackIncome = Mathf.Clamp(enemyAttackPower - inCombatPlayerStatsSO.Defense, 1, enemyAttackPower);
        inCombatPlayerStatsSO.CurrentHealth -= attackIncome;
        playerGetsHitAnimation.Raise();

        if (upgradesSelected.HasUpgrade(spinesId))
        {
            EnemyReceivesDamage(inCombatPlayerStatsSO.Attack * spinesDamageMultiplier);
        }

        if (inCombatPlayerStatsSO.CurrentHealth < 0)
        {
            if(godMode == true)
            {
                inCombatPlayerStatsSO.CurrentHealth = 1;
            }
            else
            {
                playerDeathEvent.Raise();
            }
        }

        UpdatePlayerHealthUI(-attackIncome);

    }

    private IEnumerator ShowUpgrades()
    {
        yield return new WaitForSeconds(2f);
        if(inCombatPlayerStatsSO.CurrentHealth > 0)
        {
            showUpgradeToChoose.Raise();
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

    public void FireUpgradeSelected()
    {
        fireLevel++;
    }

    // TEST FUNCTIONS
    public void WinCombatDEBUG()
    {
        TurnOffEnemyFire();
        playerWinFightEvent.Raise();
        StartCoroutine(ShowUpgrades());

        currentEnemy += 1;
        onChangeCurrentEnemy(currentEnemy + 1, levelSO.Enemies.Count);
    }
}
