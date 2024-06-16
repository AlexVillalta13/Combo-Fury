using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackUpgrade : MonoBehaviour
{
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    public void HeavyAttackUpgradeSelected()
    {
        inCombatPlayerStatsSO.HeavyAttackLevel++;
        foreach (BrickProbability brickProbability in inCombatPlayerStatsSO.BrickProbabilityList)
        {
            if (brickProbability.BrickType == BrickTypeEnum.HeavyAttackBrick)
            {
                brickProbability.Probability += 25f;
                return;
            }
        }
    }

    public void HeavyAttack()
    {
        PlayerAttacks.OnPlayerAttacks?.Invoke(this, new PlayerAttacks.OnPlayerAttacksEventArgs() { playerAttackDamage = CalculateDamage() });
    }

    private float CalculateDamage()
    {
        return inCombatPlayerStatsSO.HeavyAttackPercentageDamage * inCombatPlayerStatsSO.Attack / 100f;
    }
}
