using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EventHandler<OnChangeHealthEventArgs> onChangeEnemyHealth;

    [SerializeField] EnemyStats stats;

    [SerializeField] GameEvent playerWinFightEvent;
    [SerializeField] GameEvent playerWinLevelEvent;

    private void OnEnable()
    {
        PlayerAttacks.OnPlayerAttacks += EnemyReceivesDamage;
    }

    private void OnDisable()
    {
        PlayerAttacks.OnPlayerAttacks -= EnemyReceivesDamage;
    }

    private void EnemyReceivesDamage(object sender, PlayerAttacks.OnPlayerAttacksEventArgs eventArgs)
    {
        float playerDamage = eventArgs.playerAttackDamage;

        stats.currentHealth -= playerDamage;

        CheckWinConditions();

        onChangeEnemyHealth?.Invoke(this, new OnChangeHealthEventArgs() { healthDifference = -playerDamage });
    }

    private void CheckWinConditions()
    {
        if (stats.currentHealth <= 0)
        {
            if (stats.currentEnemy < stats.totalEnemies)
            {
                playerWinFightEvent.Raise(gameObject);
            }
            else if (stats.currentEnemy == stats.totalEnemies)
            {
                playerWinLevelEvent.Raise(gameObject);
            }

            stats.currentEnemy += 1;
        }
    }


}
