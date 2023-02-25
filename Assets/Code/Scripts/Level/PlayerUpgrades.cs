using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    CombatController m_combatController;

    [Header("Scriptable Objects Stats")]
    [SerializeField] PlayerStatsSO PermanentPlayerStatsSO;
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;
    [SerializeField] UpgradeInLevelSO upgradeInLevelSO;
    [SerializeField] UpgradeInLevelSO upgradesPlayerHasSO;

    [Header("Upgrade Stats")]
    [SerializeField] int healPercentage = 25;
    [SerializeField] int littleAttackIncreasePercentage = 5;
    [SerializeField] int mediumAttackIncreasePercentage = 10;
    [SerializeField] int mediumDefenseIncreasePercentage = 10;
    [SerializeField] int maxHealthIncreasePercentage = 10;
    [SerializeField] float criticalChanceIncrease = 5f;
    [SerializeField] int healthPercentageToActivateRage = 30;
    [SerializeField] int extraRageAttack = 20;

    // Properties
    bool hasRageUpgrade = false;
    bool hasRageState = false;

    private void Awake()
    {
        m_combatController = FindObjectOfType<CombatController>();
    }

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
        hasShield = false;
        hasRageUpgrade = false;
        hasRageState = false;
    }

    public void IncreaseMaxHealth()
    {
        int amountToIncreaseMaxHealth = PermanentPlayerStatsSO.MaxHealth * maxHealthIncreasePercentage / 100;
        inCombatPlayerStatsSO.MaxHealth += amountToIncreaseMaxHealth;
        inCombatPlayerStatsSO.CurrentHealth += amountToIncreaseMaxHealth;

        CombatController.onChangePlayerHealth(inCombatPlayerStatsSO.CurrentHealth, inCombatPlayerStatsSO.MaxHealth, amountToIncreaseMaxHealth);
    }

    public void Heal()
    {
        int amountToHeal = PermanentPlayerStatsSO.MaxHealth * healPercentage / 100;
        inCombatPlayerStatsSO.CurrentHealth += amountToHeal;
        inCombatPlayerStatsSO.CurrentHealth = Mathf.Clamp(inCombatPlayerStatsSO.CurrentHealth, 0, inCombatPlayerStatsSO.MaxHealth);

        CombatController.onChangePlayerHealth(inCombatPlayerStatsSO.CurrentHealth, inCombatPlayerStatsSO.MaxHealth, amountToHeal);
    }

    public void IncreaseAttack()
    {
        int attackIncrease = PermanentPlayerStatsSO.Attack * littleAttackIncreasePercentage / 100;
        if(attackIncrease < 1)
        {
            attackIncrease = 1;
        }
        inCombatPlayerStatsSO.Attack += attackIncrease;
    }

    public void BigAttackIncrease()
    {
        int attackIncrease = PermanentPlayerStatsSO.Attack * mediumAttackIncreasePercentage / 100;
        if(attackIncrease < 2)
        {
            attackIncrease = 2;
        }
        inCombatPlayerStatsSO.Attack += attackIncrease;
    }

    public void DefenseIncrease()
    {
        int defenseIncrease = PermanentPlayerStatsSO.Attack * mediumDefenseIncreasePercentage / 100;
        if(defenseIncrease < 2)
        {
            defenseIncrease = 2;
        }
        inCombatPlayerStatsSO.Defense += defenseIncrease;
    }

    public void IncreaseCriticalChance()
    {
        inCombatPlayerStatsSO.CriticalAttackChance += criticalChanceIncrease;
    }

    private void CheckRageCondition(int playerCurrentHealth, int playerMaxHealth, int healthDifference)
    {
        if(hasRageUpgrade == true)
        {
            if(hasRageState == false)
            {
                int healthLimitToRage = playerMaxHealth * healthPercentageToActivateRage / 100;
                if (playerCurrentHealth <= healthLimitToRage)
                {
                    int attackToIncrease = inCombatPlayerStatsSO.Attack * extraRageAttack / 100;
                    inCombatPlayerStatsSO.Attack += attackToIncrease;
                    hasRageState = true;
                }
            }
            else if(hasRageState == true)
            {
                int healthLimitToRage = playerMaxHealth * healthPercentageToActivateRage / 100;
                if (playerCurrentHealth > healthLimitToRage)
                {
                    int attackToIncrease = inCombatPlayerStatsSO.Attack * extraRageAttack / 100;
                    inCombatPlayerStatsSO.Attack -= attackToIncrease;
                    hasRageState = false;
                }
            }
        }
    }

    public void HasRageUpgrade()
    {
        hasRageUpgrade = true;
    }
}
