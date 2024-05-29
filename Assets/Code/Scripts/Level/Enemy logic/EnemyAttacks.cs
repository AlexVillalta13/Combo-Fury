using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        OnEnemyAttacks?.Invoke(this, new OnEnemyAttacksEventArgs() { enemyAttackDamage = enemyStats.attack});
    }
}
