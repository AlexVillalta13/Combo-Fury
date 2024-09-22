using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUpgrade : MonoBehaviour
{
    [SerializeField] PlayerStatsSO PermanentPlayerStatsSO;
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    [SerializeField] GameEvent onPlayerChangeInCombatStat;

    private int maxHealthLevel = 0;

    public void IncreaseMaxHealth()
    {
        maxHealthLevel++;
        float amountToIncreaseMaxHealth = maxHealthLevel * PermanentPlayerStatsSO.MaxHealthIncreasePerLevel;
        // float amountToIncreaseMaxHealth = Mathf.Round(PermanentPlayerStatsSO.MaxHealth * inCombatPlayerStatsSO.MaxHealthIncreasePercentage / 100);
        
        inCombatPlayerStatsSO.MaxHealth += amountToIncreaseMaxHealth;
        inCombatPlayerStatsSO.CurrentHealth += amountToIncreaseMaxHealth;

        PlayerHealth.onChangePlayerHealth(this, new OnChangeHealthEventArgs() { healthDifference = amountToIncreaseMaxHealth });
    }

    public void Heal()
    {
        float amountToHeal = Mathf.Round(PermanentPlayerStatsSO.MaxHealth * inCombatPlayerStatsSO.HealPercentage / 100);
        inCombatPlayerStatsSO.CurrentHealth += amountToHeal;
        inCombatPlayerStatsSO.CurrentHealth = Mathf.Clamp(inCombatPlayerStatsSO.CurrentHealth, 0, inCombatPlayerStatsSO.MaxHealth);

        PlayerHealth.onChangePlayerHealth(this, new OnChangeHealthEventArgs() { healthDifference = amountToHeal });
    }

    public void DefenseIncrease()
    {
        float defenseIncrease = Mathf.Round(PermanentPlayerStatsSO.Attack * inCombatPlayerStatsSO.MediumDefenseIncreasePercentage / 100);
        if (defenseIncrease < 2)
        {
            defenseIncrease = 2;
        }
        inCombatPlayerStatsSO.Defense += defenseIncrease;
        onPlayerChangeInCombatStat.Raise(gameObject);
    }

    public void IncreaseAttack()
    {
        float attackIncrease = Mathf.Round(PermanentPlayerStatsSO.Attack * inCombatPlayerStatsSO.LittleAttackIncreasePercentage / 100);
        if (attackIncrease < 1)
        {
            attackIncrease = 1;
        }
        inCombatPlayerStatsSO.Attack += attackIncrease;

        onPlayerChangeInCombatStat.Raise(gameObject);
    }
    
    public void IncreaseMinAttack()
    {
        float attackIncrease = Mathf.Round(PermanentPlayerStatsSO.MinAttack * inCombatPlayerStatsSO.LittleAttackIncreasePercentage / 100);
        if (attackIncrease < 1)
        {
            attackIncrease = 1;
        }
        inCombatPlayerStatsSO.MinAttack += attackIncrease;
        inCombatPlayerStatsSO.MaxAttack += attackIncrease;

        onPlayerChangeInCombatStat.Raise(gameObject);
    }
    
    public void IncreaseMaxAttack()
    {
        float attackIncrease = Mathf.Round(PermanentPlayerStatsSO.MaxAttack * inCombatPlayerStatsSO.LittleAttackIncreasePercentage / 100);
        if (attackIncrease < 1)
        {
            attackIncrease = 1;
        }
        inCombatPlayerStatsSO.MaxAttack += attackIncrease;

        onPlayerChangeInCombatStat.Raise(gameObject);
    }

    public void IncreaseCriticalChance()
    {
        inCombatPlayerStatsSO.CriticalAttackChance += inCombatPlayerStatsSO.CriticalChanceIncrease;
        onPlayerChangeInCombatStat.Raise(gameObject);
    }
}
