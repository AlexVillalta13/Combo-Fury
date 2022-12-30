using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBars : GameplayUIElement
{
    const string playerBarReference = "PlayerBar";
    const string enemyBarReference = "EnemyBar";

    VisualElement playerBar;
    VisualElement enemyBar;

    public override void Awake()
    {
        base.Awake();
    }

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        playerBar = m_UIElement.Query<VisualElement>(name: playerBarReference);
        enemyBar = m_UIElement.Query<VisualElement>(name: enemyBarReference);
    }
}
