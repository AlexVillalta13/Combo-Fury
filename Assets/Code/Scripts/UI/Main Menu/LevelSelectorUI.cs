using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LevelSelectorUI : UIComponent
{
    [SerializeField] List<LevelSO> m_Levels;
    [SerializeField] LevelSO testLevel;

    const string levelHolderReference = "levelHolder";
    const string backButtonReference = "BackButton";

    List<VisualElement> levelHoldersList = new List<VisualElement>();
    ScrollView scrollView;
    VisualElement backButton;

    public static Action<LevelSO> loadLevel;
    [SerializeField] UnityEvent onLevelSelectionMenuClosed;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        scrollView = m_UIElement.Query<ScrollView>();
        backButton = m_UIElement.Query<VisualElement>(backButtonReference);
        levelHoldersList = m_UIElement.Query<VisualElement>(className: levelHolderReference).ToList();

        SetTouchCallbacks();
    }
    private void SetTouchCallbacks()
    {
        backButton.RegisterCallback<ClickEvent>(BackButtonClicked);

        levelHoldersList[0].RegisterCallback<ClickEvent>(LoadFirstLevelCallback);
        levelHoldersList[1].RegisterCallback<ClickEvent>(LoadSecondLevelCallback);
        levelHoldersList[2].RegisterCallback<ClickEvent>(LoadThirdLevelCallback);
    }
    private void BackButtonClicked(ClickEvent evt)
    {
        onLevelSelectionMenuClosed?.Invoke();
        HideGameplayElement();
    }
    private void LoadFirstLevelCallback(ClickEvent evt)
    {
        loadLevel?.Invoke(m_Levels[0]);
        onLevelSelectionMenuClosed?.Invoke();
        HideGameplayElement();
    }
    private void LoadSecondLevelCallback(ClickEvent evt)
    {
        loadLevel?.Invoke(m_Levels[1]);
        onLevelSelectionMenuClosed?.Invoke();
        HideGameplayElement();
    }
    private void LoadThirdLevelCallback(ClickEvent evt)
    {
        loadLevel?.Invoke(m_Levels[2]);
        onLevelSelectionMenuClosed?.Invoke();
        HideGameplayElement();
    }

    private void Start()
    {
        if(testLevel != null)
        {
            loadLevel?.Invoke(testLevel);
        }
        else
        {
            loadLevel?.Invoke(m_Levels[0]);
        }
    }

    public override void ShowGameplayElement()
    {
        base.ShowGameplayElement();

        StartCoroutine(CenterScrollViewOnVisualElement(levelHoldersList[0]));
    }

    private IEnumerator CenterScrollViewOnVisualElement(VisualElement visualElementToCenterOn)
    {
        yield return null;
        float centerOffset = (scrollView.worldBound.height - visualElementToCenterOn.resolvedStyle.height) / 2;
        scrollView.scrollOffset = new Vector2(0, visualElementToCenterOn.layout.y - centerOffset);
    }
}
