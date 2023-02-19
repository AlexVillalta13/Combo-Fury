using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    CombatController m_combatController;

    [Header("Scriptable Objects Stats")]
    [SerializeField] PlayerStatsSO PermanentStatsSO;
    [SerializeField] UpgradeInLevelSO upgradeInLevelSO;
    [SerializeField] UpgradeInLevelSO upgradesPlayerHasSO;

    [Header("Upgrade Stats")]
    [SerializeField] int healPercentage = 25;
    [SerializeField] int littleAttackIncreasePercentage = 5;
    [SerializeField] int mediumAttackIncreasePercentage = 10;
    [SerializeField] int maxHealthIncreasePercentage = 10;

    // Properties
    bool hasShield = false;

    private void Awake()
    {
        m_combatController = FindObjectOfType<CombatController>();
    }

    public void ResetUpgrades()
    {
        upgradesPlayerHasSO.UpgradeList.Clear();
        hasShield = false;
    }

    public void IncreaseMaxHealth()
    {
        int amountToIncreaseMaxHealth = PermanentStatsSO.MaxHealth * maxHealthIncreasePercentage / 100;
        m_combatController.PlayerMaxHealth += amountToIncreaseMaxHealth;
        m_combatController.PlayerCurrentHealth += amountToIncreaseMaxHealth;
        m_combatController.UpdatePlayerHealthUI(amountToIncreaseMaxHealth);
    }

    public void Heal()
    {
        int amountToHeal = PermanentStatsSO.MaxHealth * healPercentage / 100;
        m_combatController.PlayerCurrentHealth += amountToHeal;
        if(m_combatController.PlayerCurrentHealth > m_combatController.PlayerMaxHealth)
        {
            m_combatController.PlayerCurrentHealth = m_combatController.PlayerMaxHealth;
        }
        m_combatController.UpdatePlayerHealthUI(amountToHeal);
    }

    public void IncreaseAttack()
    {
        int attackIncrease = PermanentStatsSO.Attack * littleAttackIncreasePercentage / 100;
        m_combatController.PlayerAttackPower += attackIncrease;
    }

    public void BigAttackIncrease()
    {
        int attackIncrease = PermanentStatsSO.Attack * mediumAttackIncreasePercentage / 100;
        m_combatController.PlayerAttackPower += attackIncrease;
    }

    public void HasShield()
    {
        hasShield = true;
    }

    public void ActivateShield()
    {
        Debug.Log("Activate shield");
        if(hasShield)
        {
            m_combatController.EnemyAttackPower = 0;
        }
    }

    public void DeactivateShield()
    {
        //Debug.Log("Deactivate shield");
        //if (hasShield == true && m_combatController.EnemyAttackPower == 0)
        //{
        //    m_combatController.SetEnemyAttack(); 
        //}
    }
}
