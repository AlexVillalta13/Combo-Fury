using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    CombatController m_combatController;

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
    [SerializeField] float healPercentage = 25;
    [SerializeField] float littleAttackIncreasePercentage = 5;
    [SerializeField] float mediumAttackIncreasePercentage = 10;
    [SerializeField] float mediumDefenseIncreasePercentage = 10;
    [SerializeField] float maxHealthIncreasePercentage = 10;
    [SerializeField] float criticalChanceIncrease = 5f;
    [SerializeField] float healthPercentageToActivateRage = 30;
    [SerializeField] float extraRageAttack = 20;

    // Properties
    bool hasRageUpgrade = false;
    bool hasRageState = false;
    public float attackPreviousToRage;

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
        hasRageUpgrade = false;
        hasRageState = false;
        attackPreviousToRage = PermanentPlayerStatsSO.Attack;
    }

    public void IncreaseMaxHealth()
    {
        float amountToIncreaseMaxHealth = PermanentPlayerStatsSO.MaxHealth * maxHealthIncreasePercentage / 100;
        inCombatPlayerStatsSO.MaxHealth += amountToIncreaseMaxHealth;
        inCombatPlayerStatsSO.CurrentHealth += amountToIncreaseMaxHealth;

        CombatController.onChangePlayerHealth(inCombatPlayerStatsSO.CurrentHealth, inCombatPlayerStatsSO.MaxHealth, amountToIncreaseMaxHealth);
    }

    public void Heal()
    {
        float amountToHeal = PermanentPlayerStatsSO.MaxHealth * healPercentage / 100;
        inCombatPlayerStatsSO.CurrentHealth += amountToHeal;
        inCombatPlayerStatsSO.CurrentHealth = Mathf.Clamp(inCombatPlayerStatsSO.CurrentHealth, 0, inCombatPlayerStatsSO.MaxHealth);

        CombatController.onChangePlayerHealth(inCombatPlayerStatsSO.CurrentHealth, inCombatPlayerStatsSO.MaxHealth, amountToHeal);
    }

    public void IncreaseAttack()
    {
        float attackIncrease = PermanentPlayerStatsSO.Attack * littleAttackIncreasePercentage / 100;
        if(attackIncrease < 1)
        {
            attackIncrease = 1;
        }
        inCombatPlayerStatsSO.Attack += attackIncrease;
        attackPreviousToRage += attackIncrease;
        onPlayerChangeInCombatStat.Raise();
    }

    public void BigAttackIncrease()
    {
        float attackIncrease = PermanentPlayerStatsSO.Attack * mediumAttackIncreasePercentage / 100;
        if(attackIncrease < 2)
        {
            attackIncrease = 2;
        }
        inCombatPlayerStatsSO.Attack += attackIncrease;
        attackPreviousToRage += attackIncrease;
        onPlayerChangeInCombatStat.Raise();
    }

    public void DefenseIncrease()
    {
        float defenseIncrease = PermanentPlayerStatsSO.Attack * mediumDefenseIncreasePercentage / 100;
        if(defenseIncrease < 2)
        {
            defenseIncrease = 2;
        }
        inCombatPlayerStatsSO.Defense += defenseIncrease;
        onPlayerChangeInCombatStat.Raise();
    }

    public void IncreaseCriticalChance()
    {
        inCombatPlayerStatsSO.CriticalAttackChance += criticalChanceIncrease;
        onPlayerChangeInCombatStat.Raise();
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
                    float attackToIncrease = attackPreviousToRage * extraRageAttack / 100;
                    inCombatPlayerStatsSO.Attack = attackPreviousToRage + attackToIncrease;
                    hasRageState = true;
                    activateRageVFX.Raise();
                    onPlayerChangeInCombatStat.Raise();
                }
            }
            else if(hasRageState == true)
            {
                float healthLimitToRage = playerMaxHealth * healthPercentageToActivateRage / 100;
                if (playerCurrentHealth > healthLimitToRage)
                {
                    inCombatPlayerStatsSO.Attack = attackPreviousToRage;
                    hasRageState = false;
                    deactivateRageVFX.Raise();
                    onPlayerChangeInCombatStat.Raise();
                }
            }
        }
    }

    public void HasRageUpgrade()
    {
        hasRageUpgrade = true;
    }
}
