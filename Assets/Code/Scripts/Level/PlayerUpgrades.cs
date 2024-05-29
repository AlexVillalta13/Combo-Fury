using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent onPlayerChangeInCombatStat;
    [SerializeField] GameEvent activateRageVFX;
    [SerializeField] GameEvent deactivateRageVFX;

    [Header("Scriptable Objects Stats")]
    [SerializeField] PlayerStatsSO PermanentPlayerStatsSO;
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;
    [SerializeField] UpgradeInLevelSO upgradeInLevelSO;
    [SerializeField] UpgradeInLevelSO upgradesPlayerHasSO;

    [Header("Upgrade Stats")]
    [SerializeField] float criticalChanceIncrease = 5f;
    [SerializeField] float healthPercentageToActivateRage = 30f;
    [SerializeField] float extraRageAttack = 20f;

    // Properties
    bool hasRageUpgrade = false;
    bool hasRageState = false;
    public float attackPreviousToRage;

    private void OnEnable()
    {
        CombatController.onChangePlayerHealth += CheckRageCondition;
    }

    private void OnDisable()
    {
        CombatController.onChangePlayerHealth -= CheckRageCondition;
    }

    public void ResetUpgrades()
    {
        upgradesPlayerHasSO.UpgradeList.Clear();
        hasRageUpgrade = false;
        hasRageState = false;
        attackPreviousToRage = PermanentPlayerStatsSO.Attack;
    }

    public void IncreaseCriticalChance()
    {
        inCombatPlayerStatsSO.CriticalAttackChance += criticalChanceIncrease;
        onPlayerChangeInCombatStat.Raise(gameObject);
    }

    private void CheckRageCondition(float playerCurrentHealth, float playerMaxHealth, float healthDifference)
    {
        if(hasRageUpgrade == true)
        {
            if(hasRageState == false)
            {
                float healthLimitToRage = playerMaxHealth * healthPercentageToActivateRage / 100;
                if (playerCurrentHealth <= healthLimitToRage)
                {
                    float attackToIncrease = Mathf.Round(attackPreviousToRage * extraRageAttack / 100);
                    inCombatPlayerStatsSO.Attack = attackPreviousToRage + attackToIncrease;
                    hasRageState = true;
                    activateRageVFX.Raise(gameObject);
                    onPlayerChangeInCombatStat.Raise(gameObject);
                }
            }
            else if(hasRageState == true)
            {
                float healthLimitToRage = playerMaxHealth * healthPercentageToActivateRage / 100;
                if (playerCurrentHealth > healthLimitToRage)
                {
                    inCombatPlayerStatsSO.Attack = attackPreviousToRage;
                    hasRageState = false;
                    deactivateRageVFX.Raise(gameObject);
                    onPlayerChangeInCombatStat.Raise(gameObject);
                }
            }
        }
    }

    public void HasRageUpgrade()
    {
        hasRageUpgrade = true;
    }

    public void FireAttackUpgradeChoosen()
    {
        inCombatPlayerStatsSO.LevelUpFireAttack();
    }
}
