using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugUI : MonoBehaviour
{
    PlayerHealth healthPlayer;
    EnemyHealth enemyHealth;

    UIDocument debugUIDocument;

    Button openDebugUIButton;
    Button closeDebugUIButton;
    VisualElement testOptionsElement;
    Toggle godModeToggle;
    Button winLevelButton;
    Button winCombatButton;

    private void Awake()
    {
        debugUIDocument = GetComponent<UIDocument>();
        healthPlayer = FindAnyObjectByType<PlayerHealth>();
        enemyHealth = FindAnyObjectByType<EnemyHealth>();

        testOptionsElement = debugUIDocument.rootVisualElement.Query<VisualElement>(name: "TestOptionsmenu");
        godModeToggle = debugUIDocument.rootVisualElement.Query<Toggle>(name: "GodModeToggle");
        openDebugUIButton = debugUIDocument.rootVisualElement.Query<Button>(name: "OpenTestOptionsButton");
        closeDebugUIButton = debugUIDocument.rootVisualElement.Query<Button>(name: "CloseButton");
        winLevelButton = debugUIDocument.rootVisualElement.Query<Button>(name: "WinLevelButton");
        winCombatButton = debugUIDocument.rootVisualElement.Query<Button>(name: "WinCombatButton");
    }

    private void OnEnable()
    {
        openDebugUIButton.clicked += EnableDebugUI;
        closeDebugUIButton.clicked += DisableTestUI;
        winCombatButton.clicked += WinCombatButtonPressed;

        godModeToggle.RegisterValueChangedCallback(GodModeCallback);
    }

    private void OnDisable()
    {
        openDebugUIButton.clicked -= EnableDebugUI;
        closeDebugUIButton.clicked -= DisableTestUI;
        winLevelButton.clicked -= WinLevelButtonPressed;
        winCombatButton.clicked -= WinCombatButtonPressed;

        godModeToggle.UnregisterValueChangedCallback(GodModeCallback);
    }

    private void GodModeCallback(ChangeEvent<bool> evt)
    {
        healthPlayer.godMode = evt.newValue;
    }

    private void WinLevelButtonPressed()
    {
        enemyHealth.WinLevelDEBUG();
    }

    private void WinCombatButtonPressed()
    {
        enemyHealth.WinCombatDEBUG();
    }

    private void EnableDebugUI()
    {
        testOptionsElement.style.display = DisplayStyle.Flex;
        openDebugUIButton.style.display = DisplayStyle.None;
    }

    private void DisableTestUI()
    {
        testOptionsElement.style.display = DisplayStyle.None;
        openDebugUIButton.style.display = DisplayStyle.Flex;
    }
}
