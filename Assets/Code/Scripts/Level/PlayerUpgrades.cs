using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] PermanentStatsSO PermanentStatsSO;
    [SerializeField] UpgradeInLevelSO upgradeInLevelSO;

    CombatController m_combatController;

    bool hasShield = false;

    private void Awake()
    {
        m_combatController = FindObjectOfType<CombatController>();
    }

    public void ResetUpgrades()
    {
        hasShield = false;
    }

    public void IncreaseMaxHealth()
    {
        Debug.Log("Max Health");

        m_combatController.PlayerMaxHealth += 10;
        m_combatController.PlayerCurrentHealth += 10;
    }

    public void Heal()
    {
        Debug.Log("Heal");
        m_combatController.PlayerCurrentHealth += 10;
        if(m_combatController.PlayerCurrentHealth > m_combatController.PlayerMaxHealth)
        {
            m_combatController.PlayerCurrentHealth = m_combatController.PlayerMaxHealth;
        }
    }

    public void IncreaseAttack()
    {
        Debug.Log("Increase attack");

        m_combatController.PlayerAttackPower += 8;
    }

    public void HasShield()
    {
        hasShield = true;
        Debug.Log("Shield");
    }

    public void ActivateShield()
    {
        m_combatController.EnemyAttackPower = 0;
    }
}
