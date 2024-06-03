using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinesUpgrade : UpgradeBehaviour
{
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    public void spinesEffect()
    {
        if(HasUpgrade() == true)
        {
            PlayerAttacks.OnPlayerAttacks?.Invoke(this, new PlayerAttacks.OnPlayerAttacksEventArgs() { playerAttackDamage = CalculateSpineDamage() });
        }
    }

    private float CalculateSpineDamage()
    {
        return inCombatPlayerStatsSO.SpinesPercentageDamage / 100f * inCombatPlayerStatsSO.Attack;
    }
}
