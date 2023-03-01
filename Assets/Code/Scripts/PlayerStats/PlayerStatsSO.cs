using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStatsSO", fileName = "New Player Stats SO")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] float maxHealth = 100;
    public float MaxHealth { set { maxHealth = value; } get { return maxHealth; } }

    [SerializeField] float currentHealth = 100;
    public float CurrentHealth { set { currentHealth = value; } get { return currentHealth; } }

    [SerializeField] float attack = 22;
    public float Attack { set { attack = value; } get { return attack; } }

    [SerializeField] float defense = 5;
    public float Defense { set { defense = value; } get { return defense; } }

    [SerializeField] float criticalAttackChance = 15f;
    public float CriticalAttackChance { set { criticalAttackChance = value; } get { return criticalAttackChance; } }
}
