using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MinMaxPlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsSo;
    
    public static EventHandler<OnPlayerAttacksEventArgs> OnPlayerAttacks;

    public class OnPlayerAttacksEventArgs : EventArgs
    {
        public float PlayerAttackDamage;
    }
    
    public void CalculatePlayerNormalAttack()
    {
        MakeAttack(GetRandomAttackNumber());
    }

    public void CalculatePlayerCriticalAttack()
    {
        float attackDamage = GetRandomAttackNumber();
        float criticalDamage = (10 * attackDamage / 100) + attackDamage;

        MakeAttack(criticalDamage);
    }

    private void MakeAttack(float attackPower)
    {
        OnPlayerAttacks?.Invoke(this, new OnPlayerAttacksEventArgs() { PlayerAttackDamage = attackPower });
    }

    private float GetRandomAttackNumber()
    {
        return Random.Range(playerStatsSo.MinAttack, playerStatsSo.MaxAttack);
    }
}
