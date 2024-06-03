using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageUpgrade : UpgradeBehaviour
{
    [SerializeField] GameEvent activateRage;
    [SerializeField] GameEvent deactivateRage;
    [SerializeField] PlayerStatsSO inCombatPlayerStats;

    private PlayerAttacks playerAttacks;

    private bool hasRage = false;

    private void Awake()
    {
        playerAttacks = GetComponentInParent<PlayerAttacks>();
    }

    private void OnEnable()
    {
        PlayerHealth.onChangePlayerHealth += ActivateRage;
    }

    private void OnDisable()
    {
        PlayerHealth.onChangePlayerHealth -= ActivateRage;
    }

    private void ActivateRage(object sender, OnChangeHealthEventArgs eventArgs)
    {
        if (PlayerHealthUnderRageCondition() == true && hasRage == false)
        {
            playerAttacks.RegisterDamageModifierInDict(this, inCombatPlayerStats.ExtraRageAttack);
            activateRage.Raise(gameObject);
            hasRage = true;
        }
        else if (PlayerHealthUnderRageCondition() == false && hasRage == true)
        {
            playerAttacks.UnregisterDamageModifierInDict(this);
            deactivateRage.Raise(gameObject);
            hasRage = false;
        }
    }

    private bool PlayerHealthUnderRageCondition()
    {
        return inCombatPlayerStats.CurrentHealth <= inCombatPlayerStats.HealthPercentageToActivateRage / 100f * inCombatPlayerStats.MaxHealth;
    }
}
