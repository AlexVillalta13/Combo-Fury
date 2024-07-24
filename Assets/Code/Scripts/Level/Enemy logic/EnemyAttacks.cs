using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStats;

    public static EventHandler<OnEnemyAttacksEventArgs> OnEnemyAttacks;

    public class OnEnemyAttacksEventArgs : EventArgs
    {
        public float enemyAttackDamage;
    }

    public void EnemyDoAttack()
    {
        float attackDamage = Random.Range(enemyStats.minAttack, enemyStats.maxAttack);
        OnEnemyAttacks?.Invoke(this, new OnEnemyAttacksEventArgs() { enemyAttackDamage = attackDamage});
    }
}
