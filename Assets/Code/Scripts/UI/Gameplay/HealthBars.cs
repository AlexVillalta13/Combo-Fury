using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBars : UIComponent
{
    const string playerBarReference = "PlayerBar";
    const string enemyBarReference = "EnemyBar";

    VisualElement playerBar;
    VisualElement enemyBar;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        CombatController.onChangePlayerHealth += ChangePlayerHealth;
        CombatController.onChangeEnemyHealth += ChangeEnemyHealth;

        HideEnemyBar();
    }

    private void OnDisable()
    {
        CombatController.onChangePlayerHealth -= ChangePlayerHealth;
        CombatController.onChangeEnemyHealth -= ChangeEnemyHealth;
    }

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        playerBar = m_UIElement.Query<VisualElement>(name: playerBarReference);
        enemyBar = m_UIElement.Query<VisualElement>(name: enemyBarReference);
    }

    public void ChangePlayerHealth(int newHealth, int maxHealt, int attackIncome)
    {
        playerBar.style.width = Length.Percent(newHealth * 100 / maxHealt);
    }

    public void ChangeEnemyHealth(int newHealth, int maxHealt, int attackIncome)
    {
        enemyBar.style.width = Length.Percent(newHealth * 100 / maxHealt);
    }

    public void ShowEnemyBar()
    {
        enemyBar.style.display = DisplayStyle.Flex;
    }

    public void HideEnemyBar()
    {
        enemyBar.style.display = DisplayStyle.None;
    }
}
