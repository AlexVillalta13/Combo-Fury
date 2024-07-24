using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    [SerializeField] LevelSO currentLevel;

    private void OnEnable()
    {
        LevelSelectorUI.onSelectedLevelToPlay += SelectCurrentLevel;
    }
    private void OnDisable()
    {
        LevelSelectorUI.onSelectedLevelToPlay -= SelectCurrentLevel;
    }

    private void SelectCurrentLevel(LevelSO level)
    {
        currentLevel = level;
        ResetLevel();
    }

    public void ResetLevel()
    {
        stats.currentEnemy = 0;
        stats.totalEnemies = currentLevel.Enemies.Count - 1;
        SetupNewEnemy();
    }

    public void SetupNewEnemy()
    {
        stats.maxHealth = currentLevel.Enemies[stats.currentEnemy].Health;
        stats.currentHealth = stats.maxHealth;
        stats.currencyRewardList = currentLevel.Enemies[stats.currentEnemy].CurrencyRewardsList;
        SetupAttackPower();
        EnemyHealth.onChangeEnemyHealth?.Invoke(this, new OnChangeHealthEventArgs() { spawnNumberTextMesh = false});
    }

    public void SetupAttackPower()
    {
        stats.attack = currentLevel.Enemies[stats.currentEnemy].Attack;
        stats.minAttack = currentLevel.Enemies[stats.currentEnemy].minAttack;
        stats.maxAttack = currentLevel.Enemies[stats.currentEnemy].maxAttack;
    }

    public void SetEnemyAttackToZero()
    {
        stats.attack = 0f;
    }
}
