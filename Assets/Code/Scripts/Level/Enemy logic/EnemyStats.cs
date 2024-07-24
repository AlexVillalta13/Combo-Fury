using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStatsSO", fileName = "New Enemy Stats SO")]
public class EnemyStats : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public float attack;

    public List<CurrencyReward> currencyRewardList = new List<CurrencyReward>();

    public int currentEnemy = 0;
    public int totalEnemies = 0;
}
