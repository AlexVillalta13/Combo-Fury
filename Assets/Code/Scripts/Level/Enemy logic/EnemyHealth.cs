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
        // PlayerAttacks.OnPlayerAttacks += EnemyReceivesDamage;
        MinMaxPlayerAttack.OnPlayerAttacks += EnemyReceivesDamage;
    }

    private void OnDisable()
    {
        // PlayerAttacks.OnPlayerAttacks -= EnemyReceivesDamage;
        MinMaxPlayerAttack.OnPlayerAttacks -= EnemyReceivesDamage;
    }

    private void EnemyReceivesDamage(object sender, MinMaxPlayerAttack.OnPlayerAttacksEventArgs eventArgs)
    {
        float playerDamage = eventArgs.PlayerAttackDamage;

        stats.currentHealth -= playerDamage;

        CheckWinConditions();

        onChangeEnemyHealth?.Invoke(this, new OnChangeHealthEventArgs() { healthDifference = -playerDamage });
    }

    private void CheckWinConditions()
    {
        if (stats.currentHealth <= 0)
        {
            if (stats.currentEnemy == stats.totalEnemies)
            {
                playerWinLevelEvent.Raise(gameObject);
            }
            else
            {
                playerWinFightEvent.Raise(gameObject);
            }

            stats.currentEnemy += 1;
        }
    }

    public void WinCombatDEBUG()
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

    public void WinLevelDEBUG()
    {
        playerWinLevelEvent.Raise(gameObject);
    }
}
