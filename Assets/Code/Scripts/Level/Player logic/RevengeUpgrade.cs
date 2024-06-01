using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengeUpgrade : UpgradeBehaviour
{
    [SerializeField] GameEvent activateRevenge;
    [SerializeField] GameEvent deactivateRevenge;

    [SerializeField] PlayerStatsSO inCombatPlayerSO;

    private PlayerAttacks playerAttacks;

    PlayerAttacks.AttackPercentageModifier attackPercentageModifierStruct = new PlayerAttacks.AttackPercentageModifier();

    private bool revengeActivated = false;

    private void Awake()
    {
        playerAttacks = GetComponentInParent<PlayerAttacks>();
    }

    private void OnEnable()
    {
        playerAttacks.RegisterAttackPower(attackPercentageModifierStruct);
        PlayerAttacks.OnPlayerAttacks += DeactivateRevenge;
    }

    private void OnDisable()
    {
        playerAttacks.UnregisterAttackPower(attackPercentageModifierStruct);
        PlayerAttacks.OnPlayerAttacks -= DeactivateRevenge;
    }

    private void CalculateRevengeDamage()
    {
        //attackDamageToSumAfterAttackStruct.percentageModifier = inCombatPlayerSO.Attack * inCombatPlayerSO.RevengePercentageIncrease / 100f - inCombatPlayerSO.Attack;
        attackPercentageModifierStruct.percentageModifier = inCombatPlayerSO.RevengePercentageIncrease;
    }

    public void ActivateRevenge()
    {
        if(HasUpgrade() == true && revengeActivated == false)
        {
            CalculateRevengeDamage();
            revengeActivated = true;
            activateRevenge.Raise(gameObject);
        }
    }

    private void DeactivateRevenge(object sender, PlayerAttacks.OnPlayerAttacksEventArgs eventArgs)
    {
        if(HasUpgrade() == true && revengeActivated == true)
        {
            attackPercentageModifierStruct.percentageModifier = 0f;
            revengeActivated = false;
            deactivateRevenge.Raise(gameObject);
        }
    }
}
