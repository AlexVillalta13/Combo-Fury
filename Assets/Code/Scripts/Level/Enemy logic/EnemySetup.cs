using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    private ILevelData currentLevel;

    private void OnEnable()
    {
        LevelSelectorUI.onSelectedLevelToPlay += SelectCurrentLevel;
    }
    private void OnDisable()
    {
        LevelSelectorUI.onSelectedLevelToPlay -= SelectCurrentLevel;
    }

    private void SelectCurrentLevel(ILevelData level)
    {
        currentLevel = level;
        ResetLevel();
    }

    public void ResetLevel()
    {
        stats.currentEnemy = 0;
        stats.totalEnemies = currentLevel.GetTotalEnemiesCount() - 1;
        SetupNewEnemy();
    }

    public void SetupNewEnemy()
    {
        stats.isBoss = currentLevel.GetEnemy(stats.currentEnemy).IsBoss;
        stats.maxHealth = currentLevel.GetEnemy(stats.currentEnemy).Health;
        stats.currentHealth = stats.maxHealth;
        stats.currencyRewardList = currentLevel.GetEnemy(stats.currentEnemy).CurrencyRewardsList;
        SetupAttackPower();
        EnemyHealth.onChangeEnemyHealth?.Invoke(this, new OnChangeHealthEventArgs() { spawnNumberTextMesh = false});
    }

    public void SetupAttackPower()
    {
        // stats.attack = currentLevel.GetEnemy(stats.currentEnemy).Attack;
        stats.minAttack = currentLevel.GetEnemy(stats.currentEnemy).minAttack;
        stats.maxAttack = currentLevel.GetEnemy(stats.currentEnemy).maxAttack;
    }

    // public void SetEnemyAttackToZero()
    // {
    //     stats.attack = 0f;
    // }
}
