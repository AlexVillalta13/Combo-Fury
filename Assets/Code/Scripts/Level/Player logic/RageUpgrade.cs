using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageUpgrade : UpgradeBehaviour
{
    [SerializeField] GameEvent activateRage;
    [SerializeField] GameEvent deactivateRage;

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
            playerAttacks.RegisterDamageModifierInDict(this, inCombatPlayerStatsSO.ExtraRageAttack);
            activateRage.Raise(this);
            hasRage = true;
        }
        else if (PlayerHealthUnderRageCondition() == false && hasRage == true)
        {
            playerAttacks.UnregisterDamageModifierInDict(this);
            deactivateRage.Raise(this);
            hasRage = false;
        }
    }

    private bool PlayerHealthUnderRageCondition()
    {
        return inCombatPlayerStatsSO.CurrentHealth <= inCombatPlayerStatsSO.HealthPercentageToActivateRage / 100f * inCombatPlayerStatsSO.MaxHealth;
    }
}
