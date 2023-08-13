using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugUI : MonoBehaviour
{
    Button openDebugUIButton;
    Button closeDebugUIButton;
    VisualElement testOptionsElement;
    Toggle godModeToggle;
    UIDocument debugUIDocument;
    CombatController combatController;

    private void Awake()
    {
        debugUIDocument = GetComponent<UIDocument>();
        combatController = FindAnyObjectByType<CombatController>();

        testOptionsElement = debugUIDocument.rootVisualElement.Query<VisualElement>(name: "TestOptionsmenu");
        godModeToggle = debugUIDocument.rootVisualElement.Query<Toggle>(name: "GodModeToggle");
        openDebugUIButton = debugUIDocument.rootVisualElement.Query<Button>(name: "OpenTestOptionsButton");
        closeDebugUIButton = debugUIDocument.rootVisualElement.Query<Button>(name: "CloseButton");
    }

    private void OnEnable()
    {
        openDebugUIButton.clicked += enableDebugUI;
        closeDebugUIButton.clicked += disableTestUI;

        godModeToggle.RegisterValueChangedCallback(GodModeCallback);
    }

    private void OnDisable()
    {
        openDebugUIButton.clicked -= enableDebugUI;
        closeDebugUIButton.clicked -= disableTestUI;

        godModeToggle.UnregisterValueChangedCallback(GodModeCallback);
    }

    private void GodModeCallback(ChangeEvent<bool> evt)
    {
        combatController.godMode = evt.newValue;
    }

    private void enableDebugUI()
    {
        testOptionsElement.style.display = DisplayStyle.Flex;
        openDebugUIButton.style.display = DisplayStyle.None;
    }

    private void disableTestUI()
    {
        testOptionsElement.style.display = DisplayStyle.None;
        openDebugUIButton.style.display = DisplayStyle.Flex;
    }
}
