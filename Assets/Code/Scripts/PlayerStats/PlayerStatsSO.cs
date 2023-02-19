using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStatsSO", fileName = "New Player Stats SO")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] int maxHealth = 100;
    public int MaxHealth { set { maxHealth = value; } get { return maxHealth; } }

    [SerializeField] int currentHealth = 100;
    public int CurrentHealth { set { currentHealth = value; } get { return currentHealth; } }

    [SerializeField] int attack = 22;
    public int Attack { set { attack = value; } get { return attack; } }

    [SerializeField] int defense = 5;
    public int Defense { set { defense = value; } get { return defense; } }

    [SerializeField] float criticalAttackChance = 15f;
    public float CriticalAttackChance { set { criticalAttackChance = value; } get { return criticalAttackChance; } }
}
