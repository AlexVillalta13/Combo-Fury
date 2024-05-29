using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public static EventHandler<OnPlayerAttacksEventArgs> OnPlayerAttacks;

    public class OnPlayerAttacksEventArgs : EventArgs
    {
        public float playerAttackDamage;
    }

    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;


    public void CalculatePlayerNormalAttack()
    {
        MakeAttack(inCombatPlayerStatsSO.Attack);
    }

    public void CalculatePlayerCriticalAttack()
    {
        float criticalDamage = (10 * inCombatPlayerStatsSO.Attack / 100) + inCombatPlayerStatsSO.Attack;

        MakeAttack(criticalDamage);
    }

    private void MakeAttack(float attackPower)
    {
        //currentComboNumber++;
        //onChangeComboNumber(currentComboNumber);

        //if (revengeCharged)
        //{
        //    attackPower *= revengeDamageMultiplier;
        //    revengeCharged = false;
        //    DeactivaRevengeVFX.Raise(gameObject);
        //}

        OnPlayerAttacks?.Invoke(this, new OnPlayerAttacksEventArgs() { playerAttackDamage = attackPower });
    }
}
