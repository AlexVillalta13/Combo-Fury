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

    public static EventHandler<OnPlayerAttacksEventArgs> OnPlayerCalculatesAttack;

    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    private Dictionary<object, float> attackExtraDamageDict = new Dictionary<object, float>();


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

        float totalDamage = CalculateTotalDamageWithModifiers(attackPower);

        OnPlayerAttacks?.Invoke(this, new OnPlayerAttacksEventArgs() { playerAttackDamage = totalDamage });
    }

    private float CalculateTotalDamageWithModifiers(float attackPower)
    {
        float totalPercentageToIncrease = 0f;

        foreach(KeyValuePair<object, float> element in attackExtraDamageDict)
        {
            totalPercentageToIncrease += element.Value;
        }
        
        //if (totalPercentageToIncrease <= 0f) { return attackPower; }
        return attackPower * totalPercentageToIncrease / 100f + attackPower;
    }

    public void RegisterDamageModifierInDict(object key, float value)
    {
        attackExtraDamageDict.Add(key, value);
    }

    public void UnregisterDamageModifierInDict(object key)
    {
        attackExtraDamageDict.Remove(key);
    }
}
