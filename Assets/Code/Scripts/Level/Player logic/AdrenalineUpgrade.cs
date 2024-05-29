using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineUpgrade : UpgradeBehaviour
{
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    private float healthPercentageToActivate = 0.3f;

    private void OnEnable()
    {
        PlayerHealth.onChangePlayerHealth += CheckForAdrenalineConditions;
    }

    private void OnDisable()
    {
        PlayerHealth.onChangePlayerHealth -= CheckForAdrenalineConditions;
    }

    private void CheckForAdrenalineConditions(object sender, OnChangeHealthEventArgs eventArgs)
    {
        if(HasUpgrade() == false) { return; }
        if (inCombatPlayerStatsSO.CurrentHealth <= healthPercentageToActivate * inCombatPlayerStatsSO.MaxHealth)
        {
            inCombatPlayerStatsSO.DodgeChance = inCombatPlayerStatsSO.AdrenalineDodgeChance;
        }
        else
        {
            inCombatPlayerStatsSO.DodgeChance = 0f;
        }
    }
}
