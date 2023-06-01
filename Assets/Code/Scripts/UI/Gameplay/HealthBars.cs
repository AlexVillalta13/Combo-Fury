using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBars : UIComponent
{
    const string playerBarReference = "PlayerFillBar";
    const string enemyBarReference = "EnemyFillBar";
    const string playerHealthTextReference = "PlayerHealthText";
    const string enemyHealthTextReference = "EnemyHealthText";
    const string playerAttackTextReference = "PlayerAttack";
    const string playerDefenseTextReference = "PlayerDefense";
    const string enemyAttackTextReference = "EnemyAttack";
    const string enemyCountTextReference = "EnemyCount";

    VisualElement playerBar;
    VisualElement enemyBar;

    Label playerHealthText;
    Label enemyHealthText;
    Label playerAttackText;
    Label playerDefenseText;
    Label enemyAttackText;

    Label enemyCountText;

    [SerializeField] PlayerStatsSO inCombatStatsSO;


    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        CombatController.onChangePlayerHealth += ChangePlayerHealth;
        CombatController.onChangeEnemyHealth += ChangeEnemyHealth;

        CombatController.onChangeEnemyAttack += ChangeEnemyAttack;

        CombatController.onChangeCurrentEnemy += ChangeEnemyCount;

        HideEnemyBar();
    }

    private void OnDisable()
    {
        CombatController.onChangePlayerHealth -= ChangePlayerHealth;
        CombatController.onChangeEnemyHealth -= ChangeEnemyHealth;

        CombatController.onChangeEnemyAttack -= ChangeEnemyAttack;

        CombatController.onChangeCurrentEnemy -= ChangeEnemyCount;
    }

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        playerBar = m_UIElement.Query<VisualElement>(name: playerBarReference);
        enemyBar = m_UIElement.Query<VisualElement>(name: enemyBarReference);

        playerHealthText = m_UIElement.Query<Label>(name: playerHealthTextReference);
        enemyHealthText = m_UIElement.Query<Label>(name: enemyHealthTextReference);

        playerAttackText = m_UIElement.Query<Label>(name: playerAttackTextReference);
        playerDefenseText = m_UIElement.Query<Label>(name: playerDefenseTextReference);
        enemyAttackText = m_UIElement.Query<Label>(name: enemyAttackTextReference);

        enemyCountText = m_UIElement.Query<Label>(name: enemyCountTextReference);
    }

    public void ChangePlayerHealth(float newHealth, float maxHealt, float attackIncome)
    {
        playerBar.style.width = Length.Percent(newHealth * 100 / maxHealt);
        playerHealthText.text = Mathf.Clamp(newHealth, 0f, maxHealt).ToString("0") + "/" + maxHealt.ToString("0");
    }

    public void ChangeEnemyHealth(float newHealth, float maxHealt, float attackIncome)
    {
        enemyBar.style.width = Length.Percent(newHealth * 100 / maxHealt);
        enemyHealthText.text = Mathf.Clamp(newHealth, 0f, maxHealt).ToString("0") + "/" + maxHealt.ToString("0");
    }

    public void ChangePlayerStats()
    {
        playerAttackText.text = inCombatStatsSO.Attack.ToString("0");
        playerDefenseText.text = inCombatStatsSO.Defense.ToString("0");
    }

    public void ChangeEnemyAttack(float newEnemyAttack)
    {
        enemyAttackText.text = newEnemyAttack.ToString("0");
    }

    public void ShowEnemyBar()
    {
        enemyBar.style.display = DisplayStyle.Flex;
    }

    public void HideEnemyBar()
    {
        enemyBar.style.display = DisplayStyle.None;
    }

    public void ChangeEnemyCount(int currentEnemy,  int enemyCount)
    {
        enemyCountText.text = currentEnemy.ToString("0") + "/" + enemyCount.ToString("0");
    }
}
