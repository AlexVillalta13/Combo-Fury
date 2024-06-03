using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengeUpgrade : UpgradeBehaviour
{
    [SerializeField] GameEvent activateRevenge;
    [SerializeField] GameEvent deactivateRevenge;

    [SerializeField] PlayerStatsSO inCombatPlayerSO;

    private PlayerAttacks playerAttacks;

    private bool revengeActivated = false;

    private void Awake()
    {
        playerAttacks = GetComponentInParent<PlayerAttacks>();
    }

    private void OnEnable()
    {
        PlayerAttacks.OnPlayerAttacks += DeactivateRevenge;
    }

    private void OnDisable()
    {
        PlayerAttacks.OnPlayerAttacks -= DeactivateRevenge;
    }

    public void ActivateRevenge()
    {
        if(HasUpgrade() == true && revengeActivated == false)
        {
            playerAttacks.RegisterDamageModifierInDict(this, inCombatPlayerSO.RevengePercentageIncrease);
            revengeActivated = true;
            activateRevenge.Raise(gameObject);
        }
    }

    private void DeactivateRevenge(object sender, PlayerAttacks.OnPlayerAttacksEventArgs eventArgs)
    {
        if(HasUpgrade() == true && revengeActivated == true)
        {
            playerAttacks.UnregisterDamageModifierInDict(this);
            revengeActivated = false;
            deactivateRevenge.Raise(gameObject);
        }
    }
}
