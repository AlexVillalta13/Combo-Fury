using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static EventHandler<OnChangeHealthEventArgs> onChangePlayerHealth;

    [SerializeField] bool godMode = false;

    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    [SerializeField] GameEvent onPlayerIsDamage;
    [SerializeField] GameEvent onPlayerIsHitWithoutDamage;
    [SerializeField] GameEvent playerDeathEvent;
    [SerializeField] GameEvent playerDodges;

    float attackIncome;

    private bool hasShield = false;

    private void OnEnable()
    {
        EnemyAttacks.OnEnemyAttacks += PlayerReceiveAttack;
    }

    private void OnDisable()
    {
        EnemyAttacks.OnEnemyAttacks -= PlayerReceiveAttack;
    }

    public void PlayerReceiveAttack(object sender, EnemyAttacks.OnEnemyAttacksEventArgs eventArgs)
    {
        if (PlayerDodgesThisAttack() == true) { return; }

        if (ShieldUpgradeBlocksAttack() == true) { return; }

        // Activate revenge

        //currentComboNumber = 0;
        //onChangeComboNumber(currentComboNumber);

        CalculateDamageIncome(eventArgs);

        // Spines

        CheckWinConditions();
    }

    private bool ShieldUpgradeBlocksAttack()
    {
        if (hasShield == true)
        {
            onPlayerIsHitWithoutDamage.Raise(gameObject);
            onChangePlayerHealth(this, new OnChangeHealthEventArgs() { healthDifference = -attackIncome });
            hasShield = false;
            return true;
        }
        return false;
    }

    private bool PlayerDodgesThisAttack()
    {
        if (UnityEngine.Random.Range(1f, 100f) < inCombatPlayerStatsSO.DodgeChance)
        {
            playerDodges.Raise(gameObject);
            return true;
        }
        return false;
    }

    private void CalculateDamageIncome(EnemyAttacks.OnEnemyAttacksEventArgs eventArgs)
    {
        attackIncome = Mathf.Clamp(eventArgs.enemyAttackDamage - inCombatPlayerStatsSO.Defense, 0f, eventArgs.enemyAttackDamage);
        inCombatPlayerStatsSO.CurrentHealth -= attackIncome;

        if(eventArgs.enemyAttackDamage == 0f)
        {
            
        }else
        {
            onPlayerIsDamage.Raise(gameObject);
        }

        onChangePlayerHealth(this, new OnChangeHealthEventArgs() { healthDifference = -attackIncome });
    }

    private void CheckWinConditions()
    {
        if (inCombatPlayerStatsSO.CurrentHealth < 0)
        {
            if (godMode == true)
            {
                inCombatPlayerStatsSO.CurrentHealth = 1;
            }
            else
            {
                playerDeathEvent.Raise(gameObject);
            }
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
    }
}