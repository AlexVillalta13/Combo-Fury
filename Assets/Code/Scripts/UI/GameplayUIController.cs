using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] UIDocument gameplayUIDocument;
    VisualElement m_gameplayRoot;

    [SerializeField] CombatBarUI combatBar;
    [SerializeField] HealthBars healthBars;
    List<UIComponent> gameplayUIElements = new List<UIComponent>();


    private void Awake()
    {
        m_gameplayRoot = gameplayUIDocument.rootVisualElement;

        gameplayUIElements.Add(combatBar);
        gameplayUIElements.Add(healthBars);

        m_gameplayRoot.style.display = DisplayStyle.None;
    }

    public void PlayGame()
    {
        m_gameplayRoot.style.display = DisplayStyle.Flex;
    }
}
