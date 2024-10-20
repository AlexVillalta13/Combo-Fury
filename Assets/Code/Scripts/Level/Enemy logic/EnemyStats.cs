using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "EnemyStatsSO", fileName = "New Enemy Stats SO")]
public class EnemyStats : ScriptableObject
{
    public bool isBoss = false;
    public float maxHealth;
    public float currentHealth;
    // public float attack;
    public float minAttack;
    public float maxAttack;

    public List<CurrencyReward> currencyRewardList = new List<CurrencyReward>();

    public int currentEnemy = 0;
    public int totalEnemies = 0;
}
